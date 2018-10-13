function App(){
  document.getElementById("addBtn").onclick = (e) => {this.addBtnClick()};
  document.getElementById("getBtn").onclick = (e) => {this.getBtnClick()};
  document.getElementById("insertAtBtn").onclick = (e) => {this.insertAtBtnClick()};
  document.getElementById("removeBtn").onclick = (e) => {this.removeBtnClick()};
  document.getElementById("removeAtBtn").onclick = (e) => {this.removeAtBtnClick()};
};

App.prototype.ajax = function(p_route, p_params){
  var v_xhttp = new XMLHttpRequest();
  var v_url = "http://localhost:3000/linkedlist/" + p_route + "?";

  if(p_params && p_params.data) v_url += "data=" + encodeURIComponent(p_params.data) + "&";
  if(p_params && p_params.index) v_url += "index=" + encodeURIComponent(p_params.index) + "&";
  v_url = v_url.substring(0,v_url.length-1);

  v_xhttp.context = this;
  v_xhttp.onreadystatechange = function() {
    if (this.readyState == 4){
      if(this.status == 200) {
        this.context.printResult(JSON.parse(this.responseText));
      } else {
        this.context.printResult({message: "Ooops something went wrong!"});
      }
    }
  };
  v_xhttp.open("GET", v_url, true);
  v_xhttp.send();
};

App.prototype.addBtnClick = function(){
  var v_data = document.getElementById("addInput").value
  this.ajax("add", {data: v_data});
};

App.prototype.getBtnClick = function(p_index){
  var v_index = document.getElementById("getInput").value;
  this.ajax("get", {index: v_index});
};

App.prototype.insertAtBtnClick = function(p_data, p_index){
  var v_data = document.getElementById("insertAtInput1").value;
  var v_index = document.getElementById("insertAtInput2").value;
  this.ajax("insertAt", {data: v_data, index: v_index});
};

App.prototype.removeBtnClick = function(){
  this.ajax("remove");
};

App.prototype.removeAtBtnClick = function(p_index){
  var v_index = document.getElementById("removeAtInput").value;
  this.ajax("removeAt", {index: v_index});
};

App.prototype.printResult = function(p_data){
  var v_result = p_data.message;
  if(p_data.node) v_result += "\n node: " + p_data.node;
  if(p_data.content) v_result += "\n content: " + p_data.content;

  document.getElementById("result").textContent = v_result;
};

var m_app = new App();
