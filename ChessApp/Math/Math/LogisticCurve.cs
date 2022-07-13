namespace CS_Math
{
    partial class MyMath
    {
        /// <summary>
        /// L = the range, K = the stepness, D = the amount the graph is futher down
        /// </summary>
        public static float LogisticCurve(float X, float L, float K, float D) // make a math folder for it self
        {
            // const double E = 2.7182818284590451; // make float
            const int E = 3; // close enough... i hope

            // float L = 30; // range, so like from 0-2
            // float K = 1; // how fast it goes to 1 or -1

            float power = (float)Math.Pow(E, -K * (X)); // use float

            L /= (1 + power);

            return L - D; // the minus makes it go from (-{L}) to {L}
        }
    }
}