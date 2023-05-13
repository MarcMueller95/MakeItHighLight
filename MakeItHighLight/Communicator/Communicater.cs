using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Communicator
{
    public class Communicater
    {

        public event Action<int> DropTrackCom;





        public void TrackDropProcess(int i)
        {
            DropTrackCom?.Invoke(i);
        }

    }
}
