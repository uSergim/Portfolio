/*
Single LinkedList implementation.
This could be written in ES6 using classes as well, 
I decided to use prototype arbitrarily.
*/

/**
 * Create a new LinkedList.
 */
function LinkedList(){
    this._head;
    this._length = 0;
};

 /**
  * Add a new node to the end of the list.
  * @param {new node's content} p_data 
  */
LinkedList.prototype.add = function(p_data){
    let v_newNode = new Node(p_data);
    let v_lastNode = this._head;

    if(!v_lastNode){
        this._head = v_newNode;
        this._length++;
    } else {
        while(v_lastNode.next){
            v_lastNode = v_lastNode.next;
        }
        v_lastNode.next = v_newNode;
        this._length++;
    }
    return;
};

/**
 * Insert a new node on given position.
 * @param {new node's content} p_data 
 * @param {new node's position} p_index
 */
LinkedList.prototype.insertAt = function(p_data, p_index){
    let v_newNode = new Node(p_data);
    let v_prevNode = this._head;
    let v_nextNode;
    let v_count = 0;
    p_index = parseInt(p_index);

    if(p_index > this._length || p_index < 0) throw "Invalid index.";

    if(p_index == 0){
        v_newNode.next = this._head;
        this._head = v_newNode;
        this._length++;
    } else{
        while(v_count < p_index-1){
            v_prevNode = v_prevNode.next;
            v_count++;
        }
        v_nextNode = v_prevNode.next;
        v_prevNode.next = v_newNode;
        v_newNode.next = v_nextNode;
        this._length++;
    }

    return;
};

/**
 * Remove this list's head.
 */
LinkedList.prototype.remove = function(){
    if(this._length > 0){
        this._head = this._head.next;
        this._length--;
    }
    
    return;
};

/**
 * Remove a node on given position
 * @param {Position of the node to be removed} p_index 
 */
LinkedList.prototype.removeAt = function(p_index){
    let v_targetNode = this._head;
    let v_prevNode;
    let v_count = 0;
    p_index = parseInt(p_index);

    if(p_index > this._length-1 || p_index < 0) return;

    if(p_index == 0){
        this.remove();
    } else {
        while(v_count < p_index){
            v_prevNode = v_targetNode;
            v_targetNode = v_targetNode.next;
            v_count++;
        }
        v_prevNode.next = v_targetNode.next;
        v_targetNode = null;
        this._length--;
    }
    return;
};

/**
 * Get a node on given position.
 * @param {Position of target node} p_index 
 */
LinkedList.prototype.get = function(p_index){
    let v_targetNode = this._head;
    let v_count = 0;
    p_index = parseInt(p_index);

    if(p_index > this._length-1 || p_index < 0) return;

    while(v_count < p_index){
        v_targetNode = v_targetNode.next;
        v_count++;
    }

    return v_targetNode;
};

/**
 * Size of the list.
 */
LinkedList.prototype.count = function(){
    return this._length;
};


function Node(p_data){
    this.data = p_data;
    this.next;
};


module.exports = LinkedList;