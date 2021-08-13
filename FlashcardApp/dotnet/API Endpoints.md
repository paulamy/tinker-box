LOGIN CONTROLLER:
Login
/login

Register
/login/register

DECKS CONTROLLER:
Get all public decks
/decks/

Get all user decks
/decks/user/:userID

Get a deck by name
/decks/:name

Get specific user deck by ID
/decks/user/:userID/deck/:deckID

Create deck
/decks/user/:userID

Update deck
/decks/user/:userID/deck/:deckID

Delete deck (changes userID to admin and marks not public)
/decks/user/:userID/deck/:deckID

CARDS CONTROLLER:
Get public cards
/cards/

Get public cards by tag
/cards/tag/:tag

Get user card by id
/cards/user/:userID/card/:cardID

Create card
/cards/user/:userID/deck/:deckID

Update card
/cards/user/:userID/card/:cardID

Delete card (changes deckID to archive deck, non-public deck owned by admin)
/cards/user/:userID/card/:cardID

SESSIONS CONTROLLER:
Get all user sessions
/sessions/user/:userID

Get specific session
/sessions/user/:userID/session/sessionID

Create session
/sessions/user/:userID

CATEGORIES CONTROLLER:
Get all categories
/categories

Get category by ID
/categories/:categoryID

Get category by name
/categories/name/:name

TAGS CONTROLLER: 
Get all tags
/tags

Get tag by ID
/tags/:tagID

Get tag by name
/tags/name/:name

CARD TYPES CONTROLLER:
Get all card types
/cardtypes

Get card type by ID
/cardtypes/:cardtypeID

Get card type by name
/cardtypes/name/:name

SESSION TYPES CONTROLLER:
Get all session types
/sessiontypes

Get session type by ID
/sessiontypes/:sessiontypeID

Get session type by name
/sessiontype/name/:name