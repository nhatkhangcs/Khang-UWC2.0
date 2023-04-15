require('dotenv').config();
const mysql = require('mysql2');

const conn = mysql.createPool("mysql://root:8FLjJbdW5r2NqkwcTT8j@containers-us-west-95.railway.app:6930/railway");

module.exports = conn;