using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andy.X.Bridge.Core.Abstractions.Services
{
    public interface IConsumer
    {
        void StartConsuming();
        void StopConsuming();
    }
}
