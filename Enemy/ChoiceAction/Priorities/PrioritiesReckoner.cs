using System;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

namespace Game.Enemy
{
    public class PrioritiesReckoner : MonoBehaviour
    {
        public enum FunctionType
        {
            InFunction,
            OutFunction,
            InOutFunction
        }

        public enum FunctionForm
        {
            Sine,
            Cubic,
            Circ,
            Elastic,
            Bounce
        }

        private class FunctionCalculator : MonoBehaviour
        {
            public Dictionary<FunctionType, Dictionary<FunctionForm, Func<double, double>>> GetFunction =
                new Dictionary<FunctionType, Dictionary<FunctionForm, Func<double, double>>>
                {
                    [FunctionType.InFunction] = new Dictionary<FunctionForm, Func<double, double>>
                    {
                        [FunctionForm.Sine] = (x) => GetInSine(x),
                        [FunctionForm.Cubic] = (x) => GetInCubic(x),
                        [FunctionForm.Circ] = (x) => GetInCirc(x),
                        [FunctionForm.Elastic] = (x) => GetInElastic(x),
                        [FunctionForm.Bounce] = (x) => GetInBounce(x)
                    },

                    [FunctionType.OutFunction] = new Dictionary<FunctionForm, Func<double, double>>
                    {
                        [FunctionForm.Sine] = (x) => GetOutSine(x),
                        [FunctionForm.Cubic] = (x) => GetOutCubic(x),
                        [FunctionForm.Circ] = (x) => GetOutCirc(x),
                        [FunctionForm.Elastic] = (x) => GetOutElastic(x),
                        [FunctionForm.Bounce] = (x) => GetOutBounce(x)
                    },

                    [FunctionType.InOutFunction] = new Dictionary<FunctionForm, Func<double, double>>
                    {
                        [FunctionForm.Sine] = (x) => GetInOutSine(x),
                        [FunctionForm.Cubic] = (x) => GetInOutCubic(x),
                        [FunctionForm.Circ] = (x) => GetInOutCirc(x),
                        [FunctionForm.Elastic] = (x) => GetInOutElastic(x),
                        [FunctionForm.Bounce] = (x) => GetInOutBounce(x)
                    },
                };

            public static double GetInSine(double x) => (1 - Cos((x * PI) / 2));

            public static double GetInCubic(double x) => x * x * x;

            public static double GetInCirc(double x) => 1 - Sqrt(1 - Pow(x, 2));

            public static double GetInElastic(double x)
            {
                const double c = (2 * PI) / 3;

                return x == 0 ? 0 :
                       x == 1 ? 1 : -Pow(2, 10 * x - 10) *
                                     Sin((x * 10 - 10.75) * c);
            }
            public static double GetInBounce(double x) => 1 - GetOutBounce(1 - x);

            public static double GetOutSine(double x) => Sin(x * PI / 2);

            public static double GetOutCubic(double x) => 1 - Pow(1 - x, 3);

            public static double GetOutCirc(double x) => Sqrt(1 - Pow(x - 1, 2));

            public static double GetOutElastic(double x)
            {
                const double c = (2 * Math.PI) / 3;

                return x == 0 ? 0 :
                       x == 1 ? 1 : Pow(2, -10 * x) *
                                    Sin((x * 10 - 0.75) * c) + 1;
            }

            public static double GetOutBounce(double x)
            {
                const double n = 7.5625;
                const double d = 2.75;

                if (x < 1 / d)
                    return n * x * x;

                else if (x < 2 / d)
                    return n * (x -= 1.5 / d) * x + 0.75;

                else if (x < 2.5 / d)
                    return n * (x -= 2.25 / d) * x + 0.9375;

                else
                    return n * (x -= 2.625 / d) * x + 0.984375;
            }

            public static double GetInOutSine(double x) => -(Cos(PI * x) - 1) / 2;

            public static double GetInOutCubic(double x) => x < 0.5 ? 4 * x * x * x : 1 - Pow(-2 * x + 2, 3) / 2;

            public static double GetInOutCirc(double x)
            {
                return x < 0.5 ? (1 - Sqrt(1 - Pow(2 * x, 2))) / 2
                               : (Sqrt(1 - Pow(-2 * x + 2, 2)) + 1) / 2;
            }

            public static double GetInOutElastic(double x)
            {
                const double c = (2 * Math.PI) / 4.5;

                return x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ?
                       -(Pow(2, 20 * x - 10)  * Sin((20 * x - 11.125) * c)) / 2
                      : (Pow(2, -20 * x + 10) * Sin((20 * x - 11.125) * c)) / 2 + 1;
            }

            public static double GetInOutBounce(double x)
            {
                return x < 0.5 ? (1 - GetOutBounce(1 - 2 * x)) / 2
                               : (1 + GetOutBounce(2 * x - 1)) / 2;
            }
        }
        FunctionCalculator _function = new FunctionCalculator();

        public Priority CalculateWithDistance(GameObject agent, Vector3 interestPosition, double radius, FunctionType type)
        {
            FunctionForm randomForm = (FunctionForm)UnityEngine.Random.Range(0, 4);

            return CalculateWithDistance(agent, interestPosition, radius, type, randomForm);
        }

        public Priority CalculateWithDistance(GameObject agent, Vector3 interestPosition, double radius, FunctionType type, FunctionForm form)
        {
            double distance = Vector3.Distance(interestPosition, agent.transform.position);
            double newValue = GetFunctionValue(type, form, distance);

            return (Priority)newValue;
        }

        private double GetFunctionValue(FunctionType function, FunctionForm type, double x) =>
            _function.GetFunction[function][type].Invoke(x);
    }
}
