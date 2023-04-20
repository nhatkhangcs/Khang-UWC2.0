const conn = require('../db/conn');
const query = require('../db/query');

function generateRandomId() {
    const min = 10000000;
    const max = 99999999;
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

async function addMessage(messageInfo) {
    //if(messageInfo.sendAt)
        let id = generateRandomId().toString();
        
        const q = "INSERT INTO message VALUES (?,?,?,?,?,?)";
        const params = [
            id,
            messageInfo.sender_id,
            messageInfo.receiver_id,
            messageInfo.date,
            messageInfo.time,
            messageInfo.content
        ];
        await query(conn, q, params);
    //}
    /*
    else {
        const q = "INSERT INTO message(sender_id, receiver_id, content) VALUES (?,?,?)";
        const params = [
            messageInfo.sender_id,
            messageInfo.receiver_id,
            messageInfo.content
        ];
        await query(conn, q, params);
    }
    */
}

async function getMessage(user1, user2) {
    return await query(
        conn, 
        'SELECT * FROM message WHERE (sender_id = ? AND receiver_id = ?) OR (sender_id = ? AND receiver_id = ?) ORDER BY date DESC', 
        [user1, user2, user2, user1]    
    );
}

module.exports = {
    addMessage,
    getMessage
}