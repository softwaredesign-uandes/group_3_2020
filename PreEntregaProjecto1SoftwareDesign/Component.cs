using System;
using System.Collections.Generic;
using System.Text;

namespace SDPreSubmissionNS {
    public interface IComponent
    {
        int Id { get; set; }

        int X { get; set; }

        int Y { get; set; }

        int Z { get; set; }

        double GetWeight();
        Dictionary<string, double> GetContinuousAtt();
        Dictionary<string, double> GetProportionalAtt();
        Dictionary<string, string> GetCategoricalAtt();

        public virtual void AddComponent(IComponent component)
        {
            throw new NotImplementedException();
        }
    }
}
