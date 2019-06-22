using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class FishBehaviour {

    // Make fish behaviours here
    public static Chain GetChain(OldFishController fishController, string fishName) {

        switch (fishName) {

            case "testFish":
                return new Chain(
                     new Action[] {fishController.Swim, fishController.Eat},
                     new Connection[] {
                         new Connection(0, 1, 1),
                         new Connection(0, 0, 1),
                         new Connection(1, 0, 1),
                         new Connection(1, 1, 1)},
                     1);


            default:
                throw new System.Exception("Invalid fish name, cannot get chain counterpart");
        }
    }




    // Rest of Code
    public delegate IEnumerator Action();

    public struct Connection {
        public int fromIndex;
        public int toIndex;
        public int lots;

        public Connection(int fromIndex, int toIndex, int lots) {
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
            this.lots = lots;
        }
    }

    public class Chain {

        public Action[] nodes;
        public Connection[] connections;
        public int currentIndex;

        public Chain(Action[] nodes, Connection[] connections, int currentIndex) {
            this.nodes = nodes;
            this.connections = connections;
            this.currentIndex = currentIndex;
        }

        public Action GetCurrentAction() {
            return nodes[currentIndex];
        }


        public void Step() {
            List<Connection> paths = new List<Connection>();
            int totalLots = 0;

            foreach (Connection possiblePath in connections) {
                if (possiblePath.fromIndex == currentIndex) {
                    paths.Add(possiblePath);
                    totalLots += possiblePath.lots;
                }
            }

            int rand = Random.Range(1, totalLots + 1);
            int currentLots = 0;
            foreach (Connection path in paths) {
                currentLots += path.lots;
                if (rand <= currentLots) {
                    currentIndex = path.toIndex;
                    break;
                }
            }
        }
    }
}
