using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Map.Models
{
    public class Centroid
    {
        private static readonly char TrimChar = '#';
        public string Code { get; private set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string VkurCd { get; set; }
        public string VkurType { get; private set; }
        public string Std { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        public double DD_N { get; private set; }
        public double DD_E { get; private set; }

        public Centroid(string[] values)
        {
            Action<string>[] PropertyMappings =
            {
                val => Code = val.Trim(TrimChar),
                val => Type = val.Trim(TrimChar),
                val => Name = val.Trim(TrimChar),
                val => VkurCd = val.Trim(TrimChar),
                val => VkurType = val.Trim(TrimChar),
                val => Std = val.Trim(TrimChar),
                val => {
                    double result = 0.0;
                    Double.TryParse(val.Trim(TrimChar).Replace('.', ','), out result);
                    X = result;
                },
                val => {
                    double result = 0.0;
                    Double.TryParse(val.Trim(TrimChar).Replace('.', ','), out result);
                    Y = result;
                },
                val => {
                    double result = 0.0;
                    Double.TryParse(val.Trim(TrimChar).Replace('.', ','), out result);
                    DD_N = result;
                },
                val => {
                    double result = 0.0;
                    Double.TryParse(val.Trim(TrimChar).Replace('.', ','), out result);
                    DD_E = result;
                }
            };

            for (int i = 0; i < values.Length; ++i)
            {
                PropertyMappings[i](values[i]);
            }
        }
    }
}
