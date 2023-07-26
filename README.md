# Auction API Endpoints

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
