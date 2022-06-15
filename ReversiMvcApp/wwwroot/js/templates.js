Handlebars.registerPartial("fiches", Handlebars.template({"1":function(container,depth0,helpers,partials,data) {
    return "<div class=\"fiche fiche__white fade-in\"></div>\r\n";
},"3":function(container,depth0,helpers,partials,data) {
    return "<div class=\"fiche fiche__black fade-in\"></div>\r\n";
},"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
    var stack1, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return ((stack1 = (lookupProperty(helpers,"ifEquals")||(depth0 && lookupProperty(depth0,"ifEquals"))||alias2).call(alias1,(depth0 != null ? lookupProperty(depth0,"beurt") : depth0),1,{"name":"ifEquals","hash":{},"fn":container.program(1, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":1,"column":0},"end":{"line":3,"column":14}}})) != null ? stack1 : "")
    + "\r\n"
    + ((stack1 = (lookupProperty(helpers,"ifEquals")||(depth0 && lookupProperty(depth0,"ifEquals"))||alias2).call(alias1,(depth0 != null ? lookupProperty(depth0,"beurt") : depth0),2,{"name":"ifEquals","hash":{},"fn":container.program(3, data, 0),"inverse":container.noop,"data":data,"loc":{"start":{"line":5,"column":0},"end":{"line":7,"column":14}}})) != null ? stack1 : "");
},"useData":true}));
Handlebars.registerHelper('ifEquals', function(arg1, arg2, options) {
                return (arg1 == arg2) ? options.fn(this) : options.inverse(this); });
this["spa_templates"] = this["spa_templates"] || {};
this["spa_templates"]["src"] = this["spa_templates"]["src"] || {};
this["spa_templates"]["src"]["templates"] = this["spa_templates"]["src"]["templates"] || {};
this["spa_templates"]["src"]["templates"]["anime"] = this["spa_templates"]["src"]["templates"]["anime"] || {};
this["spa_templates"]["src"]["templates"]["anime"]["fact"] = Handlebars.template({"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "<div class=\"d-flex flex-column\">\r\n    <h1 class=\"text-center\">"
    + alias4(((helper = (helper = lookupProperty(helpers,"anime") || (depth0 != null ? lookupProperty(depth0,"anime") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"anime","hash":{},"data":data,"loc":{"start":{"line":2,"column":28},"end":{"line":2,"column":37}}}) : helper)))
    + "</h1>\r\n    <div class=\"d-flex flex-row\">\r\n        <div class=\"col-5\"><span class=\"font-weight-bold\">Fact: </span>"
    + alias4(((helper = (helper = lookupProperty(helpers,"facts") || (depth0 != null ? lookupProperty(depth0,"facts") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"facts","hash":{},"data":data,"loc":{"start":{"line":4,"column":71},"end":{"line":4,"column":80}}}) : helper)))
    + "</div>\r\n        <img class=\"w-50 col-7\" src=\""
    + alias4(((helper = (helper = lookupProperty(helpers,"imgUri") || (depth0 != null ? lookupProperty(depth0,"imgUri") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"imgUri","hash":{},"data":data,"loc":{"start":{"line":5,"column":37},"end":{"line":5,"column":47}}}) : helper)))
    + "\" alt=\"Girl in a jacket\">\r\n    </div>\r\n </div>";
},"useData":true});
this["spa_templates"]["src"]["templates"]["feedbackWidget"] = this["spa_templates"]["src"]["templates"]["feedbackWidget"] || {};
this["spa_templates"]["src"]["templates"]["feedbackWidget"]["body"] = Handlebars.template({"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
    var helper, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "<section class=\"body\">\r\n "
    + container.escapeExpression(((helper = (helper = lookupProperty(helpers,"bericht") || (depth0 != null ? lookupProperty(depth0,"bericht") : depth0)) != null ? helper : container.hooks.helperMissing),(typeof helper === "function" ? helper.call(depth0 != null ? depth0 : (container.nullContext || {}),{"name":"bericht","hash":{},"data":data,"loc":{"start":{"line":2,"column":1},"end":{"line":2,"column":12}}}) : helper)))
    + "\r\n </section>";
},"useData":true});
this["spa_templates"]["src"]["templates"]["chart"] = this["spa_templates"]["src"]["templates"]["chart"] || {};
this["spa_templates"]["src"]["templates"]["chart"]["stats"] = Handlebars.template({"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data) {
    var helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "const myChart = new Chart(document.getElementById(\"line-chart\"), {\r\n  type: 'line',\r\n  data: {\r\n    labels: ["
    + alias4(((helper = (helper = lookupProperty(helpers,"label") || (depth0 != null ? lookupProperty(depth0,"label") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"label","hash":{},"data":data,"loc":{"start":{"line":4,"column":13},"end":{"line":4,"column":22}}}) : helper)))
    + "],\r\n    datasets: [{ \r\n        data: ["
    + alias4(((helper = (helper = lookupProperty(helpers,"wit") || (depth0 != null ? lookupProperty(depth0,"wit") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"wit","hash":{},"data":data,"loc":{"start":{"line":6,"column":15},"end":{"line":6,"column":22}}}) : helper)))
    + "],\r\n        label: \"Wit\",\r\n        borderColor: \"#E6E6FA\",\r\n        fill: false\r\n      }, { \r\n        data: ["
    + alias4(((helper = (helper = lookupProperty(helpers,"zwart") || (depth0 != null ? lookupProperty(depth0,"zwart") : depth0)) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"zwart","hash":{},"data":data,"loc":{"start":{"line":11,"column":15},"end":{"line":11,"column":24}}}) : helper)))
    + "],\r\n        label: \"Zwart\",\r\n        borderColor: \"#000000\",\r\n        fill: false\r\n      }\r\n    ]\r\n  },\r\n  options: {\r\n    title: {\r\n      display: true,\r\n      text: 'Aantal veroverd'\r\n    }\r\n  }\r\n});";
},"useData":true});
this["spa_templates"]["src"]["templates"]["spelBord"] = this["spa_templates"]["src"]["templates"]["spelBord"] || {};
this["spa_templates"]["src"]["templates"]["spelBord"]["bord"] = Handlebars.template({"1":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var stack1, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "    <tr>\r\n"
    + ((stack1 = lookupProperty(helpers,"each").call(depth0 != null ? depth0 : (container.nullContext || {}),depth0,{"name":"each","hash":{},"fn":container.program(2, data, 0, blockParams, depths),"inverse":container.noop,"data":data,"loc":{"start":{"line":4,"column":2},"end":{"line":6,"column":11}}})) != null ? stack1 : "")
    + "  </tr>\r\n";
},"2":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var stack1, helper, alias1=depth0 != null ? depth0 : (container.nullContext || {}), alias2=container.hooks.helperMissing, alias3="function", alias4=container.escapeExpression, alias5=container.lambda, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "  <td data-y='"
    + alias4(((helper = (helper = lookupProperty(helpers,"index") || (data && lookupProperty(data,"index"))) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data,"loc":{"start":{"line":5,"column":14},"end":{"line":5,"column":24}}}) : helper)))
    + "' data-x='"
    + alias4(alias5((container.data(data, 1) && lookupProperty(container.data(data, 1),"index")), depth0))
    + "' data-val=\""
    + alias4(alias5(depth0, depth0))
    + "\" onclick='Game.Reversi.doeZet("
    + alias4(((helper = (helper = lookupProperty(helpers,"index") || (data && lookupProperty(data,"index"))) != null ? helper : alias2),(typeof helper === alias3 ? helper.call(alias1,{"name":"index","hash":{},"data":data,"loc":{"start":{"line":5,"column":98},"end":{"line":5,"column":108}}}) : helper)))
    + ","
    + alias4(alias5((container.data(data, 1) && lookupProperty(container.data(data, 1),"index")), depth0))
    + ")'>"
    + ((stack1 = container.invokePartial(lookupProperty(partials,"fiches"),depth0,{"name":"fiches","hash":{"beurt":depth0},"data":data,"helpers":helpers,"partials":partials,"decorators":container.decorators})) != null ? stack1 : "")
    + "</td>\r\n";
},"compiler":[8,">= 4.3.0"],"main":function(container,depth0,helpers,partials,data,blockParams,depths) {
    var stack1, lookupProperty = container.lookupProperty || function(parent, propertyName) {
        if (Object.prototype.hasOwnProperty.call(parent, propertyName)) {
          return parent[propertyName];
        }
        return undefined
    };

  return "<table id='bord' border=1>\r\n"
    + ((stack1 = lookupProperty(helpers,"each").call(depth0 != null ? depth0 : (container.nullContext || {}),(depth0 != null ? lookupProperty(depth0,"bord") : depth0),{"name":"each","hash":{},"fn":container.program(1, data, 0, blockParams, depths),"inverse":container.noop,"data":data,"loc":{"start":{"line":2,"column":0},"end":{"line":8,"column":9}}})) != null ? stack1 : "")
    + "</table>";
},"usePartial":true,"useData":true,"useDepths":true});