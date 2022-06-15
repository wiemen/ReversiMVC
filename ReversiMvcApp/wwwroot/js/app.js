"use strict";function asyncGeneratorStep(e,t,n,a,o,i,r){try{var c=e[i](r),s=c.value}catch(e){return void n(e)}c.done?t(s):Promise.resolve(s).then(a,o)}function _asyncToGenerator(c){return function(){var e=this,r=arguments;return new Promise(function(t,n){var a=c.apply(e,r);function o(e){asyncGeneratorStep(a,t,n,o,i,"next",e)}function i(e){asyncGeneratorStep(a,t,n,o,i,"throw",e)}o(void 0)})}}function _classCallCheck(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}function _defineProperties(e,t){for(var n=0;n<t.length;n++){var a=t[n];a.enumerable=a.enumerable||!1,a.configurable=!0,"value"in a&&(a.writable=!0),Object.defineProperty(e,a.key,a)}}function _createClass(e,t,n){return t&&_defineProperties(e.prototype,t),n&&_defineProperties(e,n),Object.defineProperty(e,"prototype",{writable:!1}),e}var FeedbackWidget=function(){function t(e){_classCallCheck(this,t),this._elementId=e,this.lsName="feedback-widget"}return _createClass(t,[{key:"elementId",get:function(){return this._elementId}},{key:"show",value:function(e,t){var n=document.getElementById(this._elementId);n.style.display="block",n.innerText=e,"success"===t?(n.classList.remove("alert-danger"),n.classList.add("alert-success")):(n.classList.remove("alert-success"),n.classList.add("alert-danger")),this.log({message:e,type:t})}},{key:"hide",value:function(){document.getElementById(this._elementId).style.display="none"}},{key:"log",value:function(e){var t=JSON.parse(localStorage.getItem(this.lsName));(t=null===t?[]:t).push(e),10<t.length?t.pop():t.length,localStorage.setItem(this.lsName,JSON.stringify(t))}},{key:"removeLog",value:function(){localStorage.removeItem(this.lsName)}},{key:"history",value:function(){var t="";JSON.parse(localStorage.getItem(this.lsName)).forEach(function(e){return t+="".concat(e.type," -  ").concat(e.message," \n")}),this.show(t)}}]),t}(),Game=function(t){function n(){setInterval(function(){Game.Model._getGameState(t)},2e3)}return{init:function(e){n(),Game.Model._getGameState(t),Game.Model.init(t,$("#spelToken").text()),Game.Data.init("production"),Game.Reversi.setBoard(e),Game.Stats.setChart(),Game.API.init()}}}("http://isrever.hbo-ict.org/"),connection=(Game.Reversi={init:function(){},doeZet:function(e,t){connection.invoke("MakeMove",Game.Model.configMap.spelId,$("#spelerToken").text(),e,t)},setBoard:function(n){Game.Data.get("".concat(Game.Model.configMap.url,"api/Spel/").concat(Game.Model.configMap.spelId)).then(function(e){var e=JSON.parse(e),t=Game.Template.parseTemplate("spelBord.bord",{bord:e.Bord,spelId:Game.Model.configMap.spelId,spelerToken:$("#spelerToken").text()});document.getElementById("spel").innerHTML=t,document.getElementById("spelBeurt").innerHTML=2===e.AandeBeurt?" Zwart":" Wit",n()})},updateBoard:function(){Game.Data.get("".concat(Game.Model.configMap.url,"api/Spel/").concat(Game.Model.configMap.spelId)).then(function(e){for(var t,n=JSON.parse(e),a=document.getElementById("bord"),o=0;t=a.rows[o];o++)for(var i=0;t.cells[i];i++)n.Bord[o][i].toString()!==t.cells[i].getAttribute("data-val")&&(t.cells[i].setAttribute("data-val",n.Bord[o][i]),1==n.Bord[o][i]?t.cells[i].innerHTML='<div class="fiche fiche__white fade-in"></div>':2==n.Bord[o][i]&&(t.cells[i].innerHTML='<div class="fiche fiche__black fade-in"></div>'))})},animeFact:function(){Game.API.getAnimeFacts().then(function(e){var t=e.animes.data[Math.floor(Math.random()*e.animes.data.length)].fact,t=Game.Template.parseTemplate("anime.fact",{anime:e.animeName.replace("_"," "),facts:t,imgUri:e.animes.img});document.getElementById("animeFact").innerHTML=t})}},Game.Model=function(){var n={spelId:"",url:"",mock:{url:"api/Spel/Beurt",data:0}};return{init:function(e,t){n.url=e,n.spelId=t},_getGameState:function(){Game.Data.get("".concat(n.url,"api/Spel/BeurtKleur/").concat(n.spelId)).then(function(e){0==e?(document.getElementById("game").style.visibility="hidden",document.getElementById("waiting").style.visibility="visible"):(document.getElementById("game").style.visibility="visible",document.getElementById("waiting").style.visibility="hidden")})},configMap:n}}(),Game.Data=function(){function t(){var n=a.mock;return new Promise(function(e,t){e(n.data)})}var a={mock:{url:"api/Spel/Beurt",data:0}},n={environment:"development"};return{init:function(e){switch(n.environment=e){case"production":break;case"development":t(a.mock.url);break;default:throw new Error("environment fout")}},get:function(e){return"development"===n.environment?t():$.get(e).then(function(e){return e}).catch(function(e){console.log(e.message)})},put:function(e,t){return $.ajax({url:e,type:"PUT",contentType:"application/json",data:JSON.stringify(t)}).then(function(e){return e}).catch(function(e){console.log(e.message)})}}}(),Game.Template=function(){function n(e){var e=e.split("."),t=spa_templates.src.templates;return e.forEach(function(e){t=t[e]}),t}return{init:function(){},getTemplate:n,parseTemplate:function(e,t){return n(e)(t)}}}(),Game.API=function(){function e(){var t=n.animeList[Math.floor(Math.random()*n.animeList.length)];return Game.Data.get(n.animeFactsUri+t.anime_name).then(function(e){return{animeName:t.anime_name,animes:e}}).catch(function(e){console.log(e.message)})}function t(t){return Game.Data.get(n.animeUri).then(function(e){return n.animeList=e.data,t()}).catch(function(e){console.log(e.message)})}var n={animeUri:"",animeFactsUri:"",animeList:null};return{init:function(){n.animeFactsUri="https://anime-facts-rest-api.herokuapp.com/api/v1/",n.animeUri="https://anime-facts-rest-api.herokuapp.com/api/v1"},getAnimeFacts:function(){return null==n.animeList?t(e):e()}}}(),Game.Stats={init:function(){},updateChart:function(){Game.Data.get("".concat(Game.Model.configMap.url,"api/spel/GetStats/").concat(Game.Model.configMap.spelId)).then(function(e){var e=JSON.parse(e),t=e[e.length-1].Item1,n=e[e.length-1].Item2,e=e[e.length-1].Item3;myChart.data.labels.push(t),myChart.data.datasets[0].data.push(n),myChart.data.datasets[1].data.push(e),myChart.update()})},setChart:function(){Game.Data.get("".concat(Game.Model.configMap.url,"api/spel/GetStats/").concat(Game.Model.configMap.spelId)).then(function(e){for(var t=JSON.parse(e),n=[],a=[],o=[],i=0;i<t.length;i++)n[i]=t[i].Item1,a[i]=t[i].Item2,o[i]=t[i].Item3;var e=Game.Template.parseTemplate("chart.stats",{label:n,wit:a,zwart:o}),r=document.createElement("script");r.innerHTML=e,document.body.appendChild(r)})}},(new signalR.HubConnectionBuilder).withUrl("http://isrever.hbo-ict.org/reversiHub").configureLogging(signalR.LogLevel.Information).build());function start(){return _start.apply(this,arguments)}function _start(){return(_start=_asyncToGenerator(regeneratorRuntime.mark(function e(){return regeneratorRuntime.wrap(function(e){for(;;)switch(e.prev=e.next){case 0:return e.prev=0,e.next=3,connection.start();case 3:console.log("SignalR Connected."),e.next=10;break;case 6:e.prev=6,e.t0=e.catch(0),console.log(e.t0),setTimeout(start,5e3);case 10:case"end":return e.stop()}},e,null,[[0,6]])}))).apply(this,arguments)}var Spel={init:function(){connection.start().then(function(){connection.invoke("JoinGroup",$("#spelToken").text()).catch(function(e){console.log(e)}),connection.on("MoveFinished",function(e){Game.Reversi.updateBoard(),1==e?document.getElementById("spelBeurt").innerHTML=" Wit":2==e&&(document.getElementById("spelBeurt").innerHTML=" Zwart"),Game.Stats.updateChart()}),connection.on("PassFinished",function(e,t){Game.Reversi.updateBoard(),e||($("#passModalBody").text(t),$("#passModal").modal("toggle")),Game.Stats.updateChart()}),connection.on("GameFinished",function(e){var t="";e.winnaar=0,e.winnaar=1,t="Wit heeft gewonnen met ".concat(e.puntenWit," tegen ").concat(e.puntenZwart," punten"),$("#gameModalBody").text(t),$("#gameModal").modal("toggle")})}).catch(function(e){return console.error(e.toString())}),$("#opgeven").on("click",function(e){var t=$("#spelToken").text(),n=$("#spelerToken").text();connection.invoke("Forfeit",t,n).catch(function(e){return console.error(e.toString())}),e.preventDefault()}),$("#pas").on("click",function(e){var t=$("#spelToken").text(),n=$("#spelerToken").text();connection.invoke("Pass",t,n).catch(function(e){return console.error(e.toString())}),e.preventDefault()})}};