const puppeteer = require('puppeteer');
const readLineSync = require('readline-sync');

console.log('Bem vindo ao Bot consertador de hora'+'\n');

var url = readLineSync.question('Insira o IPV4 abaixo para continuar-mos:');

async function robo(){
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();
  await page.goto('https://'+url+'/HourFixer/');
  await page.screenshot({ path: 'example.png' });

  await browser.close();
};

robo();