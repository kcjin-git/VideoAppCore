

SELECT A.*
  FROM VOTE_QUESTION A,
       VOTE_PARTICIPANT C
 WHERE A.VOTE_ID = C.VOTE_ID
   AND A.EXPR_DATE > GetDate()
   AND VOTE_STAT != '02' --ªË¡¶
   AND C.EMAIL = 'kcjin@kt.com'
  ;

