
var vm = new Vue({
  el: '#app',
  data:{
    getIndex: null,
    addData: null,
    insertAtData: null,
    insertAtIndex: null,
    removeAtIndex: null,
    results: {}
  },
  methods:{
    callAPI: function(p_params){
      var v_url = "http://localhost:3000/linkedlist/" + p_params.route + "?";

      if(p_params.index) v_url += "index=" + p_params.index + "&";
      if(p_params.data) v_url += "data=" + p_params.data + "&";

      v_url = v_url.substring(0, v_url.length-1);

      axios.get(v_url).then((p_res) => {
        this.processAPI(p_res.data);
      }, (err) => {
        this.results.message = "Oops, something wrong happened. Did you type the necessary and correct inputs?";
        this.results.content = null;
      })
    },
    processAPI: function(p_data){
      this.results = p_data;
    }
  }
})
