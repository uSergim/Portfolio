var express = require('express');

var router = express.Router();

router.get('/linkedlist/add', function(req, res){
    var v_data = req.query.data;

    //Allows cross origin request. FOR DEBUGGING PURPOSES ONLY
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST');
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');
        
    if(!v_data) 
        return res.status(500).send({error: "Missing data information."});
    
    try{
        global.list.add(v_data);
    } catch(ex){
        return res.status(500).send({error: ex.message});
    }
    
    res.status(200).send({message: "Content added to list successfully!"});
});

router.get('/linkedlist/insertAt', function(req, res){
    var v_data = req.query.data;
    var v_index = req.query.index;

    //Allows cross origin request. FOR DEBUGGING PURPOSES ONLY
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST');
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');
        
    if(!v_index || !v_data) 
        return res.status(500).send({error: "Missing index and/or data information."});
    
    try{
        global.list.insertAt(v_data, v_index);
    } catch(ex){
        return res.status(500).send({error: ex.message});
    }
    
    res.status(200).send({message: "The data was inserted on position #" + v_index + " of the list successfully!"});
});

router.get('/linkedlist/get', function(req, res){
    var v_index = req.query.index;
    let v_node;
        
    //Allows cross origin request. FOR DEBUGGING PURPOSES ONLY
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST');
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');

    if(!v_index) 
        return res.status(500).send({error: "Missing index information."});
    
    try{
        v_node = global.list.get(v_index);
    } catch(ex){
        return res.status(500).send({error: ex.message});
    }
    
    res.status(200).send({message: "Got node #" + v_index + " from list successfully!",
                        content: v_node.data});
});

router.get('/linkedlist/remove', function(req, res){
    //Allows cross origin request. FOR DEBUGGING PURPOSES ONLY
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST');
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');

    try{
        global.list.remove();
    } catch(ex){
        return res.status(500).send({error: ex.message});
    }
    
    res.status(200).send({message: "Head removed from list successfully!"});
});

router.get('/linkedlist/removeAt', function(req, res){
    var v_index = req.query.index;

    //Allows cross origin request. FOR DEBUGGING PURPOSES ONLY
    res.setHeader('Access-Control-Allow-Origin', '*');
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST');
    res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');
            
    if(!v_index) 
        return res.status(500).send({error: "Missing index information."});
    
    try{
        global.list.removeAt(v_index);
    } catch(ex){
        return res.status(500).send({error: ex.message});
    }
    
    res.status(200).send({message: "Node #" + v_index + " removed from list successfully!"});
});

module.exports = router;