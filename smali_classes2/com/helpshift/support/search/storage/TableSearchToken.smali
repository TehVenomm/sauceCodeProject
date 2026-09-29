.class public interface abstract Lcom/helpshift/support/search/storage/TableSearchToken;
.super Ljava/lang/Object;
.source "TableSearchToken.java"

# interfaces
.implements Landroid/provider/BaseColumns;


# static fields
.field public static final COLUMN_SCORE:Ljava/lang/String; = "score"

.field public static final COLUMN_TOKEN:Ljava/lang/String; = "token"

.field public static final COLUMN_TYPE:Ljava/lang/String; = "type"

.field public static final COMMA_SEP:Ljava/lang/String; = ", "

.field public static final INT_TYPE:Ljava/lang/String; = " INTEGER "

.field public static final NOT_NULL:Ljava/lang/String; = " NOT NULL "

.field public static final PRIMARY_KEY:Ljava/lang/String; = " PRIMARY KEY "

.field public static final SQL_CREATE_TABLE:Ljava/lang/String; = "CREATE TABLE search_token_table (token TEXT  PRIMARY KEY , type INTEGER , score TEXT  NOT NULL  )"

.field public static final TABLE_NAME:Ljava/lang/String; = "search_token_table"

.field public static final TEXT_TYPE:Ljava/lang/String; = " TEXT "
