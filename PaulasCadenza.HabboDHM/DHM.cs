using PaulasCadenza.HabboDHM.ASUtilities;
using PaulasCadenza.HabboDHM.Crypto;
using PaulasCadenza.Utilities;
using System;
using System.IO;
using System.Linq;

namespace PaulasCadenza.HabboDHM
{
	public sealed class DHM : IDisposable
	{
		private const string E = "10001";
		private const string N =
			"bd214e4f036d35b75fee36000f24ebbef15d56614756d7afbd4d186ef5445f75" +
			"8b284647feb773927418ef70b95387b80b961ea56d8441d410440e3d3295539a" +
			"3e86e7707609a274c02614cc2c7df7d7720068f072e098744afe68485c629789" +
			"3f3d2ba3d7aaaaf7fa8ebf5d7af0ba2d42e0d565b89d332de4cf898d666096ce" +
			"61698de0fab03a8a5e12430cb427c97194cbd221843d162c9f3acf74da1d80eb" +
			"c37fde442b68a0814dfea3989fdf8129c120a8418248d7ee85d0b79fa818422e" +
			"496d6fa7b5bd5db77e588f8400cda1a8d82efed6c86b434bafa6d07dfcc459d3" +
			"5d773f8dfaf523dfed8fca45908d0f9ed0d4bceac3743af39f11310eaf3dff45";
		private readonly RSAKey _rsaKey = RSAKey.ParsePublicKey(N, E);

		private readonly DHMSession _dhmSession;

		public string Answer { get; }
		public string SharedSecretHexString { get; private set; }
		public byte[] SharedSecret { get; private set; }

		public DHM(string left, string right, bool randomize = true)
		{
			var arrLeft = ASByteArray.HexStringToByteArray(left);
			var arrRight = ASByteArray.HexStringToByteArray(right);

			ASByteArray arrVLeft = new ASByteArray(), arrVRight = new ASByteArray();
			_rsaKey.Verify(arrLeft, arrVLeft, (uint)arrLeft.Length);
			_rsaKey.Verify(arrRight, arrVRight, (uint)arrRight.Length);

			BigInteger biL = new BigInteger(arrVLeft.ToString(), 10),
				biR = new BigInteger(arrVRight.ToString(), 10);

			if (biL.Equals(biR) || (biL.CompareTo(BigInteger.Nbv(2)) <= 0) || (biR.CompareTo(biL) >= 0))
			{
				throw new InvalidDataException("Invalid DHM prime and generator");
			}

			_dhmSession = new DHMSession(biL, biR);

			string answer = null, lastRandomString = null, finalRandomString = null;
			var index = 10;
			while (index > 0)
			{
				lastRandomString = GenerateString(30, randomize);
				_dhmSession.Init(lastRandomString);
				var radixAnswer = _dhmSession.ApplyRadix1(10);
				if (radixAnswer.Length < 64)
				{
					if ((answer == null) || (radixAnswer.Length > answer.Length))
					{
						answer = radixAnswer;
						finalRandomString = lastRandomString;
					};
				}
				else
				{
					answer = radixAnswer;
					finalRandomString = lastRandomString;
					break;
				};
				index--;
			};
			if (lastRandomString != finalRandomString)
			{
				_dhmSession.Init(finalRandomString);
			};

			ASByteArray answerBuffer = new ASByteArray(), encryptedBuffer = new ASByteArray();
			answerBuffer.WriteMultiByte(answer, 28591); // iso-8859-1, System.Text.Encoding.Latin1, code page 28591
			_rsaKey.Encrypt(answerBuffer, encryptedBuffer, (uint)answerBuffer.Length, randomize);

			Answer = ASByteArray.ByteArrayToHexString(encryptedBuffer);
		}

		public void ApplyServerAnswer(string answer)
		{
			var arrAnswer = ASByteArray.HexStringToByteArray(answer);
			var arrAnswerEnc = new ASByteArray();

			_rsaKey.Verify(arrAnswer, arrAnswerEnc, (uint)arrAnswer.Length);

			_dhmSession.Init2(arrAnswerEnc.ToString(), 10);
			var sharedKey = _dhmSession.ApplyRadix2(16);

			if (!_dhmSession.Compare1() || !_dhmSession.Compare2())
			{
				throw new InvalidDataException("Server answer is invalid");
			}

			var arrSharedKey = ASByteArray.HexStringToByteArray(sharedKey);
			SharedSecretHexString = ASByteArray.ByteArrayToHexString(arrSharedKey);
			SharedSecret = ASByteArray.ByteArrayToManagedByteArray(arrSharedKey);
		}

		private static string GenerateString(int count, bool randomize = true) =>
			string.Concat(Enumerable.Range(0, count).Select(x => randomize ? $"{PRNG.Instance.Next(256):x2}" : "55"));

		public void Dispose()
		{
			_dhmSession.Dispose();
			_rsaKey.Dispose();
			SharedSecretHexString = null;
			SharedSecret = null;
		}

		private sealed class TestData
		{
			public string Left { get; set; }
			public string Right { get; set; }
			public string Answer { get; set; }
			public string ServerAnswer { get; set; }
			public string SharedSecret { get; set; }
		};

		public static bool TestCase() =>
			new TestData[]
			{
				new TestData
				{
					Left = "b771ebbc8443ff5946547d86d1ba2de013face112518adc1eccc44a2d1bccafaa82f9243fc2c054a6db85be7f90ff1a786bbd8699aa47f96d6e80f0d73294faa1e7553b39275b4b9e7db59bab7178673d0a9fbebaa6f5f427ea40f524ce00b092773d56cfbb7d85f597120805fc3b6b7753130bc94d83244152662c586ef0fbc0aa2a64455f301e6a3fd2a260f9ad574a0656a7f8cc106fd274ec32af01b1558ecb5d35c45b2dcff9b34c50154e251f2d732df83145626ac03df00425221e94c11a8891dc3d5082412df96a385a31d1dd0a6bee5f4215b9d921186c3b835a51945170315d6708757569adb20c8ee0922e1a81e84fc13bbbc5303e06a7be15b79",
					Right = "9cd66500bd69d84a7d3f612bb30e09db34cf318f14727b9b82298ccbf29273f7f03758bd7b688d7cee68a5aa3bf8dbb048fc7c54ea01303f61376cd040e749a5c35d0c2306a32df9aba8791aa9b65d270d77416da3b35392c6aa3e714be1cda7ea49f5028bc009c8dae1ec4090c697a92addd51a480e528ced8776aa92ea757630174fcc040c1b20fb3ba495c81d6acf77f41d1bfee3ef9f17c0d1b2595d4c445fe994f2201211c579c7f6e00377e9a07bf49c3875c95d226c9d40fea5e99cfb86868bce88fff49a9739c3366dc49b3b8741fce1dcf1bb03269fad302a8c43799023e9ecc5d6a583a612109b9d0f2e1e7e10ea5307444cfd061c5109426f2a78",
					Answer = "38d4feff3beca0b3825820a199b0d9f3fda16b43818e8d38000ebb26358647137844a616c7603e5f78eebabe07ad971127769cba2f86b7088d10d33df68bbe40a23c00440d01d885441e6cd0c6fe427b3566752e806291bc305f09dbc8076faadb35c8270b7e2405a01747b431cd543d1c8c60f4624db62e60e2234041b9bac518b0668c64c1950d961a8360190b5700ee16d280c48db226618dc6f316171d26b9cedf01c44b133cfcea7918b4fb5b50040e6aec5178d49049b2f845738da6fb5f2a26b22c70fe57c20458e0f99c5cc23177a18d34c6f56b6888b62cc69a8eee78d2bb63f4b85c1384332da26a203b7ce9ff071b2752970d4b3922c954fdf2fa",
					ServerAnswer = "4225f5cf4150e1c5c9b4e581460b985a11e2b7a9bcfb544a1be08b733b0920a0f82028e765d67f39ddc0e5074188c8db7171712771c15fda6a9420b634461b039d2748fd7751c5dfada05bd3f0186cd5367322f64bf2bc419303ec9b18b33c895df6bf0f8ad0a4fba775c6f8bc5b0307b95abd9fe7bfa867863f56d9df83c0cec312603a9dba7441cc8fadd07b33278a3ef93eca95298db6d7f0e323ae94cc03101cbc55d6346aff35409e8cdf173dd8def7fe059192fba2de37df8a4d0f8ea85706164a5776a1cbf3d55d47920293008644f7d8c69391399d11c2a7642c57849c7ff4bfa496b1d679cb763773292bc44e929503aba5f464bfaf867443eb4dd6",
					SharedSecret = "060a327c335861216d7fd259dc2633647650932d5f6b37f6cf54da"
				},
				new TestData
				{
					Left = "1d0f9e06ec0f0ddd47554e90487b084ce5d6e99255cdbf2d5014da3bd9c9afe997e7a415d3cafc94cf8d19592db4bbf165cb853a9c87d4eef8d3d53371c6fe3a9f86144b30c942878055cb55eb80175a24cf11aca99a78bedd5120bf108fc1ab477838b67e7a1f4b17e6d48fce29e9c48595661b8cee19328840dbb034329f946f272be0694a85ea490cb2a816f476a00235912970d56fa048ecb9fead56387d08d71e0c43b6961bfb1b9325622b491aaf41c09e89a266d44585e1308dceef47b8561e91da8ad2adabdc5db424e28d548fcc2a585c7df49fe40bf96c1537b1ed4d4fd15127b89789d33a477e41b2576a644899253576fcab453432822d0ec88f",
					Right = "b1f08ba9e4eb9c72af0035aaa2ea76b23dff97a00b61f5c803c1cf805bab186c8d209b0ee92a4637d9b10d5e0dedf05367c808c30eaff73926a006fbe6506cdd842a83f4e7cf3f274cc80cce77c90a4db145873c58ba82c5696750b61c53f358658dae358348b7593fad3747cb43d100d63fccb78bb0b3b4033620f51891bc9ef0d9095799f3c3c9ef5ec2d68f339e709738ef36fdb4da8c6c2080bbb068cb4656a56ea703ef57fe80aaddd41eb08cfb4886fc76bece2e88ed01703aac05ba7e70d0486f535de2b6597521185921dd9eeecafc36e8c9d59d04ea246531c8ccd7de133951fc76bf4d7dfa95550347adbb51ea3546487654a5881fd05b6757f651",
					Answer = "09f433dbf0d1ece685f58ea80ab2fde6c0d42d4065c96aee30376af5505941534a1ff5019b5587a23f6e54fa7c28085245f665f264ccde9a5fea62678891a11ef2079810319c4f4a0d9c4150a23e100b84220626bfb8fa5b2fa57af70be8e236cfb306ed70ccab7a8e4c4ac613d71e651d600f83e454ccfc0c06be53e6450d2e96ca8503bee2c390ac24401322fcd72b2d48fe2c29d1bd9ff1f5b0785df1ac46cb277064518195c51312356b7700533d4e69040674364c2228e3da2e820d64d3a654076fdfdf5a45e324b98693cdbd103a542bba8bda9c624fc878a6c5eb30676ce994de8023a4bca2cf7ceeae9705d02e44ae636d76946095324708afc78d57",
					ServerAnswer = "5c1625d59ac5989d5232bb0b361623414b1d454add1241c42133703d4f1b1f4fbe526344cc333bd036647b927a3c713a39a84f811bf42b791a5f5c586c0d905b9881537799e0985f96c6d61bac5b5377f7f4f3ac2990a49439917f124e151df76b62b25a307be47a326be6f3591ea299840b65e05279996f871c2477f60d8da080c3e5986a3450aed4f7d0388445834c09ebfba566f2fde41106c987a99860dfb0dc7f86bd1baf5441de05c08716338e9855d6b06c2ad353ec985ab902006a08ba55ae032af769b64887f0237f3527ae48d56284dabcfd6b7dcf8b861a9744392bc2ae10c31bc9c2f11e3538a80ebb802afc68d9f316f8e180bb90edbf5f97dd",
					SharedSecret = "7faea2e06ad91dad228ad2185f9f3082a07f344aee274b8f4eea"
				},
				new TestData
				{
					Left = "1ac58668ab6bd6b6bb04cd7156da356379ca237d4cf51db260b10494e53c6095f33cdb4b36628a528e6af3c52414f954b00a39e556cebdbe49c48a590ff9aba49f1a932cf2b80fb27346cb27c10c74e886a614c30b10def4a396cf11ffb8536f2efb5f780d61e8767c0e7b750db93fda207546328e51fd958b9339d66c520652b25d6d4a63517f72c5941b21808b8238658aca1e026ebc0b85624cfac657e5498bf50146bd13f7a6f7ea6093191a85c996b3a5f3c2024fe8d3d882c862188597675f8221ed9e3a281d7bffc7cad9db3de449ab3de6448793f9486e83688560ce61cb35676bcbc426b2d63b554af70b1b36d41d6bcf18cfb8943d698a5b44729c",
					Right = "3d58c9b3a88a7bed85107a0d2239548551a228370b801b6fbe3a0726d6d10adca86f34f611e408dc5754b90412a592a5e8e87b42dc5ad15804b16c6097711292900c06053a426bf104cb0b41f83ff94cc31363aa7edac4ef0a03d7243f546659d978ff52b8c52806f6c40dd80b3f3ec9fc6d9e763d4bfb89c4623432f26901fb7ac5514a49d6c3d3adb1cddcff0635841556fb9239447a04cb4bb1188dfbc226e680475fe1575245f29b523e018442bd37821e384f29529d7e410f8a0bcfb3ed6030cd8dc89f86e8b8424b2e388852e37aeb5f6eb8c6998dfc80f939773dadceb0e7340ed6f0734e194f52068e355c1d3cf268f5638e705a5346fd971ea2d42a",
					Answer = "30f85b8f7079a8bdb88bd959c66bb78ce3e4d51a38db55209bab8b4dace74f82217a6770bbd16d4e7bc493b865ef1f26507b12a76a9fd5df126be7bff7003b65a7104298b848362834e2e6e35cdbafaacf1af07bfa6ba360f5df3a91b32128aec958bf339b6f91f3b37579d627f172a8e5a605025eba468c7eae4861d686b5d0a24a5abda03769c65daa388b9a227796ec657b3ea89eaa90b968d296449b8a366b6b1c7c8b06fb97987437be0634398ca87dd754f6ab49166c21b11ebe56ce45b906e2eac05c4c454d41ba3fc790db83d627e0908b5bf85c32954e0fbd1ff7ef237e7e2f260c921c24e0c514f575772b3ddd4c2d671a8cc3407219c6d69eaac7",
					ServerAnswer = "7fcdfc0d9417ce8837f78df428d44c3e7b5723981341ea31b403745472698f60ca447079d98c359c6194734444ea32e41c3f290f744e3ccedfa1d5dfa95e237dc86d311cc1ba2b06b67df7fdff7abec8350e8116cc0012211c7dd5e9887fda0d711d2d2d53b21d42659fb7704e41713359ddf22b5d05a15247ea202ee8fd85b5ec66b7ab445655320567ad3115cbafdd82a8d605f528a184b7bd593aa6ce104e415f80739802a4156491132a0c06931a9723fde4830d5c39e7ec49d596ed90a64ba73c11d46328475b3aa6914af45e836ffc6dbd8b0c55df5ad3c23f6498db63223be3190cc9f377d57f5941eb729c8ec12ae3ca120534e1c4c2f13c04fe7795",
					SharedSecret = "0437f51c7540909f3aa6595753cc8056babe7706399177f41375e7"
				},
				new TestData
				{
					Left = "50ff112dbddf4f9b742c93d876bb44b5e091c14c8e579f9ebb4e277232ec9c9813d34128800858a659fa0f7dd30fe7a021bec73441111cf3db80e0003e012e2d11727b31699605c9f329b5f0f7d23d2122bde5939b8ecf2968d233784a2194baad134ef247c1618f01f0800123d730e87f1fc00824e53813f551714140b76e167f5c1c15f08e98906f23881138c6176d5ce3799a246ce9cc1e2f9ded62fa170c2980f8a20d7f6162054f11f41a943873a0f9df6bb41fc46e30cfc06f012ff07723b133a759d59436606e3c2fcb36713c3bcbb6751903dcde1ab1db87fac7c7b54a98726c9e5b19bfd4bd84505a8608ce875386593735dd20b4c38075fdebadaa",
					Right = "b743fcd01e87603efdeeaa37aaba358f79bef372966b207c3b3b883a2d559136ab187431f5b7237d148a58ef023e428e049e1d84a6de67169643a6ecf1043978d2282e203efe1481b8a5d574744cc311ae2ed6ccbaff40957d296b1d302f25eaed2ad77dcad70f809b8ca6d8d693542b85b4d0081707efd0c1299de460eb6f2281b7c5e6120e3f8e70cbc55a44c42ee80b65d1b35795483824cdbfddbe4e7aa56be93f37f63cf579c53a0ca306c28098c4a765e0aeeb5ded2a9164ae5bd8c9567980df0c5a60b415abef6d771ce2d1ef91b4072de0ba8b4a7e07806f576d317cc5e662298569ae500c7caf4d4c9018e8467c980c5675f86ff41a15752f96a83b",
					Answer = "2606f296ee665079602ffe53000ff43ab3ee1ed44093390d605932cab92a9a7f9e1f63b1b77ae97eb3f800deca5f06baa6bb2d9c9c9f18a934517e983311fc46347b048f46cd3517aa2856f560429ada935b2bc4bca693346d2d606c6826964ed004520dd15487e8e8ba43202679c9de524381ad0f44710befa2b7ad4e586a081f0550fc88a8292b38dea98c3a7aae3b3b836cab1bd71579600806cde85454ae8885bca2eda3b64d8c7a5e21a7ddbd20bffe17e3f6d2df0c82a6d14c8b6be974ba2b59ae31e74d2917242108451a59f02292a3dc37d33fc94889ff2d76678cb03fae71772292c4eaf5e2481db4c43db9b3c492f2bd4d6860265f47a5c33cc43c",
					ServerAnswer = "285d6b11c4ae101e7ef162ea0f657ea198617e8f87e999368713ce961610b054ab76297b2ce9723d195e0cd286a07e41620b824ee4fc099237249845bb46b41e97e8d1024c290f5f6426af9993a1e9df42cb27fcda3885d1bd6ff1c1b25cb21ad7f4cbe615b1ca96144157e54e2a302a846d0e175bca01c3d744e6858718110b88d4edbd7d3ff7dff835da16b18d9cdba890a1e562d4bb8dd3e8f84f4788d638352cafb4e096937aaceac8fb25165049ce533bed7f8b8bd1627d8af32f52b8e6690fa568a941380367f6f6e7f8b09a6a479a5e121da6ef7cdaffb6be4d0418bbc924dc2c0dfbf5be1af6b177e7d9dcd21def0d665b33ea3a6bd84c2cff669c29",
					SharedSecret = "0d4effbce7556967982e407b69c39f324e93056882dc765b2cfd77"
				}
			}.All(x =>
			{
				var t = new DHM(x.Left, x.Right, randomize: false);
				if (t.Answer == x.Answer)
				{
					t.ApplyServerAnswer(x.ServerAnswer);
					return t.SharedSecretHexString == x.SharedSecret;
				}
				return false;
			});
	}
}
