require('dotenv').config();

 function authenticateToken(req, res, next) {
    // // extract token from header
    // const authHeader = req.headers['authorization'];
    // const token = authHeader && authHeader.split(' ')[1];
    // // no token was found
    // if(token == null) return res.sendStatus(401);

    // // verify token
    // jwt.verify(token, process.env.ACCESS_TOKEN_SECRET, (err, user) => {
    //     if (err) return res.sendStatus(403);
    //     req.user = user;
    //     next();
    // })

    next();
};

module.exports = authenticateToken;