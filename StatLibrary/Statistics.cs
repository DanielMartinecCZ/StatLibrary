namespace StatLibrary
{
    public static class Statistics
    {


        ///<summary>
        ///Returns mean of an numeric array
        ///</summary>
        public static double Mean<T>(T[] values) where T : IComparable<T>
        {
            DescribeExceptions(values);
            return values.Average(x => Convert.ToDouble(x));
        }
        ///<summary>
        ///Returns rounded mean of an numeric array
        ///</summary>
        public static double Mean<T>(T[] values, int roundDigits) where T : IComparable<T>
        {
            DescribeExceptions(values, roundDigits);
            IsNumberInRange(roundDigits);
            return Math.Round(values.Average(x => Convert.ToDouble(x)), roundDigits);
        }

        ///<summary>
        ///Returns median of an numeric array
        ///</summary>
        public static double Median<T>(T[] values) where T : IComparable<T>
        {
            DescribeExceptions(values);

            values = SortValues(values);
            int medInd = values.Length / 2;
            if (values.Length % 2 == 0)
                return (Convert.ToDouble(values[medInd]) + Convert.ToDouble(values[medInd - 1])) / 2;
            return Convert.ToDouble(values[medInd]);
        }

        ///<summary>
        ///Returns rounded median of an numeric array
        ///</summary>
        public static double Median<T>(T[] values, int roundDigits) where T : IComparable<T>
        {
            DescribeExceptions(values);
            IsNumberInRange(roundDigits);

            values = SortValues(values);
            int medInd = values.Length / 2;
            if (values.Length % 2 == 0)
                return (Convert.ToDouble(values[medInd]) + Convert.ToDouble(values[medInd - 1])) / 2;
            return Math.Round(Convert.ToDouble(values[medInd]), roundDigits);
        }

        ///<summary>
        ///Find most frequently occuring numeric value
        ///</summary>
        ///<returns>Most frequently occurring numeric value or most frequently occurring numeric value and its count in dictionary</returns>
        public static dynamic Modus<T>(T[] values, bool includeCount = false) where T : IComparable<T>
        {

            DescribeExceptions(values);

            Dictionary<T, int> counts = new Dictionary<T, int>();
            foreach (var item in values)
            {
                if (counts.ContainsKey(item))
                {
                    counts[item]++;
                    continue;
                }
                counts[item] = 1;
            }

            int max = counts[counts.Keys.First()];
            T key = counts.Keys.First();

            foreach (var item in counts.Keys)
                if (counts[item] > max)
                {
                    max = counts[item];
                    key = item;
                }

            if (includeCount)
                return new Dictionary<T, int>() { { key, max } };
            return key;
        }

        ///<summary>
        ///Returns largest value in array
        ///</summary>
        public static T Max<T>(T[] values) where T : IComparable<T>
        {
            DescribeExceptions(values);
            return values.Max();
        }

        ///<summary>
        ///Returns smallest value in array
        ///</summary>
        public static T Min<T>(T[] values) where T : IComparable<T>
        {
            DescribeExceptions(values);
            return values.Min();
        }

        ///<summary>
        ///Sorts the elements in array
        ///</summary>
        ///<returns> Sorted array</returns>
        public static T[] SortValues<T>(T[] values) where T : IComparable<T>
        {

            DescribeExceptions(values);
            Array.Sort(values);
            return values;
        }

        ///<summary>
        ///Sorts the elements in array
        ///</summary>
        ///<returns> Sorted array</returns>
        public static T[] SortValues<T>(T[] values, bool asc) where T : IComparable<T>
        {
            DescribeExceptions(values);
            if (asc)
                return values.OrderBy(x => x).ToArray();
            else
                return values.OrderByDescending(x => x).ToArray();
        }

        ///<summary>
        ///Returns the difference between largest and smallest number in array
        ///</summary>
        public static T Range<T>(T[] values) where T : IComparable<T> => (dynamic)Max(values) - (dynamic)Min(values);

        ///<summary>
        ///Returns number expressing by how much the elements of an array differ from the mean value
        ///</summary>
        public static double StandardDeviation<T>(T[] values) where T : IComparable<T>
        {
            DescribeExceptions(values);
            double mean = Mean(values);
            double sum = values.Sum(x => Math.Pow((Convert.ToDouble(x) - Convert.ToDouble(mean)), 2));
            sum = sum / values.Count();
            return Math.Sqrt(sum);
        }

        ///<summary>
        ///Returns rounded number expressing by how much the elements of an array differ from the mean value
        ///</summary>
        public static double StandardDeviation<T>(T[] values, int roundDigits) where T : IComparable<T>
        {
            DescribeExceptions(values);
            IsNumberInRange(roundDigits);

            double mean = Mean(values);
            double sum = values.Sum(x => Math.Pow((Convert.ToDouble(x) - Convert.ToDouble(mean)), 2));
            sum = sum / values.Count();
            return Math.Round(Math.Sqrt(sum), roundDigits);
        }

        ///<summary>
        ///Returns number expressing mean of deviations
        ///</summary>
        public static double AverageDeviation<T>(T[] values) where T : IComparable<T>
        {
            DescribeExceptions(values);
            double mean = Mean(values);
            return values.Sum(x => Math.Abs(Convert.ToDouble(x) - mean)) / values.Length;
        }

        ///<summary>
        ///Returns rounded number expressing mean of deviations
        ///</summary>
        public static double AverageDeviation<T>(T[] values, int roundDigits) where T : IComparable<T>
        {
            DescribeExceptions(values);
            IsNumberInRange(roundDigits);
            double mean = Mean(values);
            return Math.Round(values.Sum(x => Math.Abs(Convert.ToDouble(x) - mean)) / values.Length, roundDigits);
        }

        /// <summary>
        /// Returns number expressing the ratio of the standard deviation and the mean
        /// </summary>
        public static double CoefficientOfVariation<T>(T[] values) where T : IComparable<T>
        {
            DescribeExceptions(values);
            return StandardDeviation(values) / Mean(values);
        }

        /// <summary>
        /// Returns rounded number expressing the ratio of the standard deviation and the mean
        /// </summary>
        public static double CoefficientOfVariation<T>(T[] values, int roundDigits) where T : IComparable<T>
        {
            DescribeExceptions(values);
            IsNumberInRange(roundDigits);

            return Math.Round(StandardDeviation(values) / Mean(values), roundDigits);
        }
        /// <summary>
        ///Returns weighted mean of an numeric array
        /// </summary>
        public static double WeightedMean<T, U>(T[] values, U[] weights) where T : IComparable<T> where U : IComparable<U>
        {
            DescribeExceptions(values, weights);
            double sum = 0;
            double n = weights.Sum(x => Convert.ToDouble(x));
            for (int i = 0; i < values.Count(); i++)
                sum += Convert.ToDouble(values[i]) * Convert.ToDouble(weights[i]);
            return sum / n;
        }

        /// <summary>
        ///Returns rounded weighted mean of an numeric array
        /// </summary>
        public static double WeightedMean<T, U>(T[] values, U[] weights, int roundDigits) where T : IComparable<T> where U : IComparable<U>
        {
            DescribeExceptions(values, weights);
            IsNumberInRange(roundDigits);

            double sum = 0;
            double n = weights.Sum(x => Convert.ToDouble(x));
            for (int i = 0; i < values.Count(); i++)
                sum += Convert.ToDouble(values[i]) * Convert.ToDouble(weights[i]);
            return Math.Round(sum / n, roundDigits);
        }

        /// <summary>
        ///Returns geometric mean of an numeric array
        /// </summary>
        public static double GeometricMean<T>(T[] values) where T : IComparable<T>
        {
            DescribeExceptions(values);

            double product = values.Aggregate((double)1, (x, y) => Convert.ToDouble(x) * Convert.ToDouble(y));
            return Math.Pow(product, (double)1 / values.Length);
        }

        /// <summary>
        ///Returns rounded geometric mean of an numeric array
        /// </summary>
        public static double GeometricMean<T>(T[] values, int roundDigits) where T : IComparable<T>
        {
            DescribeExceptions(values);
            IsNumberInRange(roundDigits);

            double product = values.Aggregate((double)1, (x, y) => Convert.ToDouble(x) * Convert.ToDouble(y));
            return Math.Round(Math.Pow(product, (double)1 / values.Length), roundDigits);
        }

        /// <summary>
        ///Returns harmonic mean of an numeric array
        /// </summary>
        public static double HarmonicMean<T>(T[] values) where T : IComparable<T>
        {
            DescribeExceptions(values);

            double sum = values.Sum(x => 1 / Convert.ToDouble(x));
            return values.Length / sum;
        }

        /// <summary>
        ///Returns rounded mean of an numeric array
        /// </summary>
        public static double HarmonicMean<T>(T[] values, int roundDigits) where T : IComparable<T>
        {
            DescribeExceptions(values);
            IsNumberInRange(roundDigits);

            double sum = values.Sum(x => 1 / Convert.ToDouble(x));
            return Math.Round(values.Length / sum, roundDigits);
        }



        #region Private Functions

        private static void DescribeExceptions<T>(T[] values) where T : IComparable<T>
        {
            if (!IsNumericArray(values))
                throw new ArgumentException("Inserted array is not numerical");
            if (values == null)
                throw new ArgumentNullException("Inserted array is null");
            if (values.Length == 0)
                throw new ArgumentException("Inserted array is empty");
            if (values.Rank > 1)
                throw new RankException("Inserted array is not one dimensional");
        }
        private static void DescribeExceptions<T>(T[] values, params object[] args) where T : IComparable<T>
        {
            if (!IsNumericArray(values))
                throw new ArgumentException("Inserted array is not numerical");
            if (values == null)
                throw new ArgumentNullException("Inserted array is null");
            if (values.Length == 0)
                throw new ArgumentException("Inserted array is empty");
            if (values.Rank > 1)
                throw new RankException("Inserted array is not one dimensional");
            foreach (var item in args)
                if (item == null)
                    throw new ArgumentNullException($"Argument {nameof(item)} is null or empty");
        }

        private static void DescribeExceptions<T, U>(T[] values, U[] values2) where T : IComparable<T> where U : IComparable<U>
        {
            if (!IsNumericArray(values) || !IsNumericArray(values2))
                throw new ArgumentException("At least one inserted array is not numerical");
            if (values.Count() != values2.Count())
                throw new ArgumentException("Lengths of arrays are not identical");
            if (values.Rank > 1)
                throw new RankException("Inserted array is not one dimensional");
            if (values == null || values2 == null)
                throw new ArgumentNullException("At least one array is null");
            if (values.Length == 0 || values2.Length == 0)
                throw new ArgumentNullException("At least one array is null");
        }
        private static void IsNumberInRange(int number, int lLimit = 0, int uLimit = 15, bool include = true)
        {
            if (include)
                if (number < lLimit || number > uLimit)
                    throw new ArgumentException($"Number of digits of rounded number cannot be less than {lLimit} nor greater than {uLimit}");
                else
                if (number < lLimit || number >= uLimit)
                    throw new ArgumentException($"Number of digits of rounded number cannot be less than {lLimit} nor greater or equal to {uLimit}");
        }


        private static bool IsNumericArray<T>(T[] array) where T : IComparable<T>
        {
            return (IsSameType(array, "System.SByte[]") || IsSameType(array, "System.Byte[]") || IsSameType(array, "System.UInt16[]") || IsSameType(array, "System.Int16[]")
                || IsSameType(array, "System.UInt32[]") || IsSameType(array, "System.Int64[]") || IsSameType(array, "System.UInt64[]") || IsSameType(array, "System.UInt64[]")
                || IsSameType(array, "System.Single[]") || IsSameType(array, "System.Double[]") || IsSameType(array, "System.Decimal[]") || IsSameType(array, "System.Int32[]"));
        }
        private static bool IsSameType<T>(T[] array, string type) where T : IComparable<T> => Convert.ToString(typeof(T[])) == type;
        #endregion

    }
}