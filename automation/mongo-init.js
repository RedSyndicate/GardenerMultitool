print('START #################################################################');

// Get the database
db = new Mongo().getDB("gardeners-multitool");

// Create the collections
db.createCollection('plant', { capped: false });
db.createCollection('location', { capped: false });
db.createCollection('zipcodehardinesszone', { capped: false })

print('END #################################################################');