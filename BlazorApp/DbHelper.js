// wwwroot/js/indexedDb.js

window.indexedDbHelper = {
    async ensureDatabaseCreated(dbName, storeName) {
        return new Promise((resolve, reject) => {
            const openRequest = indexedDB.open(dbName, 1);

            openRequest.onupgradeneeded = function (event) {
                const db = event.target.result;
                if (!db.objectStoreNames.contains(storeName)) {
                    db.createObjectStore(storeName, { keyPath: "id", autoIncrement: true });
                }
            };

            openRequest.onerror = function (event) {
                reject("Error opening IndexedDB: " + event.target.error);
            };

            openRequest.onsuccess = function (event) {
                resolve("Database created or already exists.");
            };
        });
    }
};
