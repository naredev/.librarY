using MunicipalApp;
using System;
using System.Collections.Generic;

namespace MunicipalApp
{
    public class ServiceRequestManager
    {
        private AVLTree<ServiceRequest> requestsTree;  // AVL Tree for fast RequestID lookup
        private MinHeap<ServiceRequest> requestsHeap;  // Min-Heap for earliest requests
        private Graph<ServiceRequest> requestGraph;    // Graph for managing dependencies

        public ServiceRequestManager()
        {
            requestsTree = new AVLTree<ServiceRequest>(request => request.RequestID);
            requestsHeap = new MinHeap<ServiceRequest>();
            requestGraph = new Graph<ServiceRequest>();
        }

        public void AddRequest(ServiceRequest request)
        {
            requestsTree.Insert(request);   // AVL Tree insertion
            requestsHeap.Insert(request);   // Heap insertion
            requestGraph.AddNode(request);  // Graph node for dependencies

            // Add edges based on dependencies
            foreach (var dep in request.Dependencies)
            {
                var depRequest = requestsTree.Search(dep);
                if (depRequest != null)
                {
                    requestGraph.AddEdge(depRequest, request);
                }
            }
        }

        public ServiceRequest FindRequest(int requestId)
        {
            return requestsTree.Search(requestId);
        }


        public List<ServiceRequest> GetUpcomingRequests()
        {
            return requestsHeap.GetAll() ?? new List<ServiceRequest>(); // Return earliest requests by date
        }
    }
}
//Part of this code was adapted from stackoverflow
//https://stackoverflow.com/questions/10796509/implementation-of-priority-queue-by-avl-tree-data-structure
//Accessed 28 October 2024