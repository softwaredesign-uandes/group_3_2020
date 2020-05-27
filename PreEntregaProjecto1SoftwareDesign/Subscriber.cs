using System;
using System.Collections.Generic;
using System.Text;

namespace SDPreSubmissionNS {
    public interface ISubscriber
    {
        void Update(string data);
    }
}
