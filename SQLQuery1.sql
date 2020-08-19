

SELECT * FROM DBO.REPORTS

SELECT DISTINCT STRT_DATE, END_DATE, ORGN_NAME, PART_NAME, REPORT_TITLE
    , STUFF((
        SELECT '<p>' + USER_NAME + '</p>' + REPORT_CONTENT
        FROM REPORTS T1
        where T1.PART_NAME = T2.PART_NAME
        FOR XML PATH('')
    ), 1, 1, '') AS REPORT_CONTENT
FROM REPORTS T2
WHERE PART_NAME = 'AntBot'


 
 SELECT * FROM COMMON_CODES WHERE GROUP_CODE = 'ORG001_MDL' AND EXPR_DATE >= GetDate();


SELECT DISTINCT STRT_DATE, END_DATE, ORGN_NAME, MODULE_NAME, REPORT_TITLE
                                        , STUFF((SELECT '\&lt;p&gt;' + USER_NAME + '&lt;/p&gt;' + REPORT_CONTENT
                                                   FROM REPORTS T1
                                                  WHERE T1.MODULE_NAME = T2.MODULE_NAME
                                                    AND T1.STRT_DATE = T2.STRT_DATE
                                                    FOR XML PATH('') 
                                                ), 1, 2, '') AS REPORT_CONTENT
                                        , STUFF((SELECT '&lt;p&gt;' + USER_NAME + '&lt;/p&gt;' + REPORT_CONTENT_EX
                                                   FROM REPORTS TX
                                                  WHERE TX.MODULE_NAME = T2.MODULE_NAME
                                                    AND TX.STRT_DATE = T2.STRT_DATE
                                                    FOR XML PATH('') 
                                                ), 1, 2, '') AS REPORT_CONTENT_EX
                                     FROM REPORTS T2

                                     select * from reports
                                    WHERE MODULE_NAME = 'AntBot' 
                                    AND CONVERT(DATE, '2020-08-19') BETWEEN STRT_DATE AND END_DATE

                                