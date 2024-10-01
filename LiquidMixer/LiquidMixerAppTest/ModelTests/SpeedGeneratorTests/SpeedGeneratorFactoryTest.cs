using LiquidMixerApp.Model.SpeedGenerator;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Management.Deployment.Preview;

namespace LiquidMixerAppTest.ModelTests.SpeedGeneratorTests
{


    internal class SpeedGeneratorFactoryTest
    {


        static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(BasicSpeedsMode.FAST, new FastSpeedGenerator());
                yield return new TestCaseData(BasicSpeedsMode.NORMAL, new NormalSpeedGenerator());
                yield return new TestCaseData(BasicSpeedsMode.SLOW, new SlowSpeedGenerator());
                yield return new TestCaseData(BasicSpeedsMode.INCREMENTAL, new IncrementalSpeedGenerator(1, 2, 1));
            }
        }


        [Test, TestCaseSource(nameof(TestCases))]
        public void GetSpeedGenerator_ShouldReturnCorrectly(BasicSpeedsMode mode, ISpeedGenerator expectedSpeedGenerator)
        {
            var _speedGeneratorFactory = new SpeedGeneratorFactory();

            var speedGenerator = _speedGeneratorFactory.GetSpeedGenerator(mode);


            ClassicAssert.IsInstanceOf(expectedSpeedGenerator.GetType(), speedGenerator);


        }
    }
}
