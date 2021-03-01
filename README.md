# Lazy Evaluator
1. Evaluator is constrained to accept numeric types only and hence the following generic constraints are applied on it;
   struct, IComparable, IComparable<T>, IConvertible, IEquatable<T>, IFormattable.
   
2. The "Add" method on the Evaluator allows for args to be null but will throw if the func passed in is null.

3. Divide by zero exceptions have been specifically handled.

4. The ExpressionArgsMapper maintains a mapping between the func and it's args.