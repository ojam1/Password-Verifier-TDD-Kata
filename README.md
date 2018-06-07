# A Solution for [Roy Osherove's TDD Kata 3](http://osherove.com/tdd-kata-3-refactoring/)

## TDD Kata 3

### Create a Password verifications class called “PasswordVerifier”.

Create a Password verifications class called “PasswordVerifier”.
1. Add the following verifications to a master function called “Verify()”
    1. password should be larger than 8 chars
    2. password should not be null
    3. password should have one uppercase letter at least
    4. password should have one lowercase letter at least
    5. password should have one number at least
Each one of these should throw an exception with a different message of your choosing
1. Add feature: Password is OK if at least three of the previous conditions is true
2. Add feature: password is never OK if item 1.4 is not true.