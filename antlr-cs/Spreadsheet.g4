grammar Spreadsheet;

// we put parentheses there to prioritize them in parsing
                                                                // these on the right are labels
                                                                // they are used to make ANTLR generate specific
                                                                // functions for the visitor or listener
expression          : '(' expression ')'                        #parenthesisExp
                    | expression (ASTERISK|SLASH) expression    #mulDivExp
                    | expression (PLUS|MINUS) expression        #addSubExp
                    // right-associativity: execute the one on the right first and then the one on the left
                    | <assoc=right>  expression '^' expression  #powerExp
                    | NAME '(' expression ')'                   #functionExp
                    | NUMBER                                    #numericAtomExp
                    | ID                                        #idAtomExp
                    ;

fragment LETTER     : [a-zA-Z] ;
fragment DIGIT      : [0-9] ;

ASTERISK            : '*' ;
SLASH               : '/' ;
PLUS                : '+' ;
MINUS               : '-' ;

ID                  : LETTER DIGIT ;

NAME                : LETTER+ ;

NUMBER              : DIGIT+ ('.' DIGIT+)? ;

WHITESPACE          : ' ' -> skip;
