const fs = require('fs');

fs.readFile('./src/index.html','utf8',(err,data)=>{
  var regex = new RegExp('<% version %>','g');
  var html = data.replace(regex,'?v='+new Date().getTime());
  fs.writeFileSync('./index.html',html,{encoding:'utf8'});
});
