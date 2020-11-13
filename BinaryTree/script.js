class Node {
    constructor(data) {
        this.data = data;
        this.left = null;
        this.right = null;
    }
}

class BinarySearchTree {
    constructor() {
        this.root = null;
    }

    addNode(data) {
        if (typeof data === 'number' && isFinite(data)) {
            let newNode = new Node(data);
            if (this.root === null) {
                this.root = newNode;
            } else {
                this.insertNode(this.root, newNode);
            }
        } else {
            console.log("incorrect value");
        }
    }

    insertNode(node, newNode,parent) {
        if (newNode.data < node.data) {
            if (node.left === null) {
                node.left = newNode;
            } else {
                parent = node;
                this.insertNode(node.left, newNode);
            }
        } else {
            if (node.right === null) {
                node.right = newNode;
            } else {
                parent = node;
                this.insertNode(node.right, newNode);
            }
        }
    }

    NLR(node) {
        if (node != null) {
            console.log(node.data);
            this.NLR(node.left);
            this.NLR(node.right);
        }
    }

    LNR(node) {
        if (node != null) {
            this.LNR(node.left);
            console.log(node.data);
            this.LNR(node.right);
        }
    }

    LRN(node) {
        if (node != null) {
            this.LRN(node.left);
            this.LRN(node.right);
            console.log(node.data);
        }
    }

    contains_(node, data, parent) {

        if (node === null) { return null; }

        switch (data < node.data ? -1 : (data > node.data ? 1 : 0)) {
            case 1: {
                parent = node;
                return this.contains_(node.right, data, parent);
            }
            case -1: {
                parent = node;
                return this.contains_(node.left, data, parent);
            }
            case 0: {
                console.log(node);
                return [parent, node];
            }
        }
    }

    contains(data) {
        if (typeof data === 'number' && isFinite(data)) {

            if (this.contains_(this.root, data)) {
                console.log("tree contain " + data);
            } else {
                console.log("tree does not contain " + data);
            }
        } else {
            console.log("incorrect value");
        }
    }

    findMinNode(node) {
        if (node.left === null)
            return node;
        else
            return this.findMinNode(node.left);
    }

    removeNode(data) {
        var nodeArray = this.contains_(this.root, data);
        var node = nodeArray[1];
        var parent = nodeArray[0];

        if (node.left === null && node.right === null) {
            parent.right = null;
            parent.left = null;
            return node;
        }

        if (node.left === null) {
            if (parent.right === node) {
                parent.right = node.right
            }
            if (parent.left === node) {
                parent.left = node.right
            }
            return node;
        } else if (node.right === null) {
            if (parent.right === node) {
                parent.right = node.left
            }
            if (parent.left === node) {
                parent.left = node.left
            }
            return node;
        }

        var newNode = this.findMinNode(node.right);
        node.data = newNode.data;
        node.right = this.removeNode(node.right, newNode.data);
        return node;
    }
}

var bst = new BinarySearchTree();
bst.addNode(10);
bst.addNode(5);
bst.addNode(2);
bst.addNode(6);
bst.addNode(19);
bst.addNode(17);
bst.addNode(21);
bst.addNode(1);
bst.contains(99);
bst.contains(6);
bst.LNR(bst.root);
bst.NLR(bst.root);
bst.LRN(bst.root);
bst.removeNode(2)
console.log(bst);

