const envSettings = {
    dev: {
        apiUrl: 'https://localhost:7048',
        socketVendor: 'socket.io-client'
    },

    staging: {
        apiUrl: 'https://localhost:7048',
        socketVendor: 'socket.io-client'
    },

    prod: {
        apiUrl: 'https://localhost:7048',
        socketVendor: 'socket.io-client'
    }
};

export const ENV = 'dev';
export const settings = envSettings[ENV];