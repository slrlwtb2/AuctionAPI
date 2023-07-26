# Auction API Endpoints
The Auction API provides endpoints for managing an auction system. The API supports creating, updating, and deleting categories and products. Users can place bids on products, and sellers can be rated by users.

This API does not contain authorization or authentication. It serves as a mock-up lightweight API for my API learning progress.


## Endpoints Summary

### Category
- Allows creating, retrieving, updating, and deleting categories.

### Product
- Allows creating, retrieving, updating, and deleting products.
- Users can place bids on products, and bidable products can be retrieved.

### User
- Allows creating, retrieving, updating users.
- Users can check their balance and increase/decrease their balance.
- Users can rate sellers.

### Seller
- Allows creating and retrieving sellers.
- Sellers can be rated.


## CategoryController

- **CreateCategory**: An endpoint that allows creating a new category with a specified name and description.
- **GetCategories**: An endpoint that retrieves the list of all categories.
- **UpdateCategory**: An endpoint that allows updating an existing category's name and description by providing the categoryId and new values.
- **DeleteCategory**: An endpoint that allows deleting a category by providing the categoryId.

## ProductController

- **GetProducts**: An endpoint that retrieves the list of all products.
- **UpdateProduct**: An endpoint that allows updating an existing product's name, description, categoryId, and startingPrice by providing the productId and new values.
- **CreateProduct**: An endpoint that allows creating a new product with a specified name, description, categoryId, sellerId, startingPrice, and bidEndTime.
- **DeleteProduct**: An endpoint that allows deleting a product by providing the productId.
- **BidProduct**: An endpoint that allows users to place bids on a product by providing the userId, productId, and bid amount.
- **GetBidableProducts**: An endpoint that retrieves the list of products that are still open for bidding based on their bidEndTime.

## UserController

- **GetUsers**: An endpoint that retrieves the list of all users.
- **GetBalance**: An endpoint that retrieves the balance of a user by providing the userId.
- **CreateUser**: An endpoint that allows creating a new user with a specified username and optional email.
- **IncreaseBalance**: An endpoint that allows increasing a user's balance by a specified amount, provided the userId.
- **DecreaseBalance**: An endpoint that allows decreasing a user's balance by a specified amount, provided the userId.
- **GiveRatingToSeller**: An endpoint that allows users to give ratings to sellers by providing the sellerId and a rating between 0 and 5.

## SellerController

- **CreateSeller**: An endpoint that allows creating a new seller (a type of user) with a specified username and optional email.
- **GetSellers**: An endpoint that retrieves the list of all sellers.
- **GetRating**: An endpoint that retrieves the rating of a seller by providing the sellerId.
