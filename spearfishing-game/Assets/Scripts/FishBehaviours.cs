using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FishBehaviour {


    public struct BehaviourConnection {
        public int fromIndex;
        public int toIndex;
        public float lots;

        public BehaviourConnection(int fromIndex, int toIndex, float lots) {
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
            this.lots = lots;
        }
    }
    

    public class BehaviourChain {

        public string[] nodes;
        public BehaviourConnection[] connections;
        public int startIndex;

        public BehaviourChain(string[] nodes, BehaviourConnection[] connections, int startIndex) {



        }





    }
}
