.class public enum Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;
.super Ljava/lang/Enum;
.source "JsonValueFormat.java"


# annotations
.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum COLOR:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum DATE:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum DATE_TIME:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum EMAIL:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum HOST_NAME:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum IPV6:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum IP_ADDRESS:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum PHONE:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum REGEX:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum STYLE:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum TIME:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum URI:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

.field public static final enum UTC_MILLISEC:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;


# direct methods
.method static constructor <clinit>()V
    .locals 15

    .line 14
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$1;

    const-string v1, "DATE_TIME"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$1;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->DATE_TIME:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 24
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$2;

    const-string v1, "DATE"

    const/4 v3, 0x1

    invoke-direct {v0, v1, v3}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$2;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->DATE:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 34
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$3;

    const-string v1, "TIME"

    const/4 v4, 0x2

    invoke-direct {v0, v1, v4}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$3;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->TIME:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 45
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$4;

    const-string v1, "UTC_MILLISEC"

    const/4 v5, 0x3

    invoke-direct {v0, v1, v5}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$4;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->UTC_MILLISEC:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 54
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$5;

    const-string v1, "REGEX"

    const/4 v6, 0x4

    invoke-direct {v0, v1, v6}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$5;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->REGEX:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 63
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$6;

    const-string v1, "COLOR"

    const/4 v7, 0x5

    invoke-direct {v0, v1, v7}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$6;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->COLOR:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 72
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$7;

    const-string v1, "STYLE"

    const/4 v8, 0x6

    invoke-direct {v0, v1, v8}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$7;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->STYLE:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 80
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$8;

    const-string v1, "PHONE"

    const/4 v9, 0x7

    invoke-direct {v0, v1, v9}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$8;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->PHONE:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 88
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$9;

    const-string v1, "URI"

    const/16 v10, 0x8

    invoke-direct {v0, v1, v10}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$9;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->URI:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 96
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$10;

    const-string v1, "EMAIL"

    const/16 v11, 0x9

    invoke-direct {v0, v1, v11}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$10;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->EMAIL:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 103
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$11;

    const-string v1, "IP_ADDRESS"

    const/16 v12, 0xa

    invoke-direct {v0, v1, v12}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$11;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->IP_ADDRESS:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 111
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$12;

    const-string v1, "IPV6"

    const/16 v13, 0xb

    invoke-direct {v0, v1, v13}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$12;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->IPV6:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    .line 119
    new-instance v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$13;

    const-string v1, "HOST_NAME"

    const/16 v14, 0xc

    invoke-direct {v0, v1, v14}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$13;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->HOST_NAME:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    const/16 v0, 0xd

    .line 8
    new-array v0, v0, [Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->DATE_TIME:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v2

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->DATE:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v3

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->TIME:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v4

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->UTC_MILLISEC:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v5

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->REGEX:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v6

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->COLOR:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v7

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->STYLE:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v8

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->PHONE:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v9

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->URI:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v10

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->EMAIL:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v11

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->IP_ADDRESS:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v12

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->IPV6:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v13

    sget-object v1, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->HOST_NAME:Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    aput-object v1, v0, v14

    sput-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->$VALUES:[Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    .line 8
    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method synthetic constructor <init>(Ljava/lang/String;ILcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat$1;)V
    .locals 0

    .line 8
    invoke-direct {p0, p1, p2}, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;-><init>(Ljava/lang/String;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;
    .locals 1

    .line 8
    const-class v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    return-object p0
.end method

.method public static values()[Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;
    .locals 1

    .line 8
    sget-object v0, Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->$VALUES:[Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    invoke-virtual {v0}, [Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/fasterxml/jackson/databind/jsonFormatVisitors/JsonValueFormat;

    return-object v0
.end method
