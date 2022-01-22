using System.Collections.Generic;

namespace Game.Enemy
{
    public class FunctionMoveDerivative
    {
        private List<(float X, float Y)> _coordinates;
        private FunctionDistance _function = new FunctionDistance();

        public void SetFunction()
        {
            _coordinates = new List<(float X, float Y)>();
            int count = _function.Elements.Count;

            for (int j = 0, i = count / 2; i <= count; i++, j++)
            {
                float valueX = _function.Elements[j].Time;
                float valueY = _function.Elements[j].Distance;

                _coordinates.Add((valueX, valueY));
            }
        }
        public float GetDerivativeFirstOrder(int pointCount) => GetDeltaY(pointCount) / GetDeltaX(pointCount);

        public float GetDerivativeSecondOrder(int pointCount) => (GetDeltaY(pointCount) - GetDeltaY(pointCount - 1)) /
                                                                  GetDeltaX(pointCount);

        public float GetDerivativeThirdOrder(int pointCount) => (GetDeltaY(pointCount) - GetDeltaY(pointCount - 1) -
                                                                 GetDeltaY(pointCount - 2)) / GetDeltaX(pointCount);

        private float GetDeltaX(int pointCount)
        {
            (float firstX, float secondX) = (_coordinates[pointCount - 1].X,
                                             _coordinates[pointCount].X);

            float deltaX = secondX - firstX;

            return deltaX;
        }

        private float GetDeltaY(int pointCount)
        {
            (float firstY, float secondY) = (_coordinates[pointCount - 1].Y,
                                             _coordinates[pointCount].Y);

            float deltaY = secondY - firstY;

            return deltaY;
        }
    }
}