\c real_estate_db

DELETE FROM areas;

INSERT INTO areas ("areaId", name, description, schools, shops, kindergardens) VALUES
(1, 'Greenfield', 'A peaceful and family-friendly area.', 'Greenfield Primary, Riverside High', 'Greenfield Mall, Local Market', 'Sunny Days Kindergarten, Little Steps'),
(2, 'Lakeside', 'Beautiful views and vibrant community.', 'Lakeside School, Mountain View High', 'Lakeside Shopping Center, Organic Foods Market', 'Happy Tots, Kids Adventure Kindergarten'),
(3, 'Downtown', 'Busy and bustling city life.', 'City School, Central High', 'Downtown Plaza, 24/7 Supermarket', 'Urban Kids, Future Leaders Kindergarten');

DELETE FROM houses;

INSERT INTO houses ("areaId", description, price, address, postcode, "sqrFeet", rooms, bathrooms, "parkingSpaces", furnished) VALUES
(1, 'Cozy family home with a large backyard.', 250000, '123 Maple Street', '12345', 2000, 3, 2, 1, false),
(1, 'Modern home near the park.', 300000, '456 Oak Avenue', '12345', 2500, 4, 3, 2, true),
(2, 'Lakeside apartment with stunning views.', 350000, '789 Lake Road', '23456', 1500, 2, 2, 1, true),
(3, 'Downtown apartment, perfect for city living.', 400000, '101 Main Street', '34567', 1000, 1, 1, 0, false);
