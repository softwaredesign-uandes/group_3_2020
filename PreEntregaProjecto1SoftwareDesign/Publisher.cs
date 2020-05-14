using System;
using System.Collections.Generic;
using System.Text;

namespace SDPreSubmissionNS {
    [Serializable]
    public class Publisher
    {

        public List<ISubscriber> Subscribers;

        public Publisher()
        {
            Subscribers = new List<ISubscriber>();
        }

        public void Subscribe(ISubscriber subscriber)
        {
            Subscribers.Add(subscriber);
        }

        public void Notify(string data)
        {
            foreach (var subscriber in Subscribers)
            {
                subscriber.Update(data);
            }
        }
    }
}
