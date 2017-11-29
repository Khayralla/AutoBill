//module.exports = function (callback, html) {
//    var jsreport = require('jsreport-core')();

//    jsreport.init().then(function () {
//        return jsreport.render({
//            template: {
//                content: html,
//                engine: 'jsrender',
//                recipe: 'phantom-pdf'
//            }
//        }).then(function (resp) {
//            callback(/* error */ null, resp.content.toJSON().data);
//        });
//    }).catch(function (e) {
//        callback(/* error */ e, null);
//    });
//};


module.exports = function (callback, data) {
    var jsreport = require('jsreport-core')();

    jsreport.init().then(function () {
        return jsreport.render({
            template: {
                content: '{{:foo}}',
                //content: '<h1>{{:foo}}</h1>',
                engine: 'jsrender',
                recipe: 'phantom-pdf'
            },
            data: {
                foo: data
            }
        }).then(function (resp) {
            callback(/* error */ null, resp.content.toJSON().data);
        });
    }).catch(function (e) {
        callback(/* error */ e, null);
    });
};