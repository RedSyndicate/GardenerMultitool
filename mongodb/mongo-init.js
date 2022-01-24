print('START #################################################################');

var plants = cat("./docker-entrypoint-initdb.d/plant.json");
var po = JSON.parse(plants);

// Get the database
db = new Mongo().getDB("gardeners-multitool");

// Create the collections
db.createCollection('plant', { capped: false });
db.createCollection('location', { capped: false });

db.plant.insertMany(po);

print('END #################################################################');