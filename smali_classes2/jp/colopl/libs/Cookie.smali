.class public Ljp/colopl/libs/Cookie;
.super Ljava/lang/Object;
.source "Cookie.java"


# static fields
.field private static a:Ljava/lang/String; = ""


# direct methods
.method static constructor <clinit>()V
    .locals 0

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 3
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static getCookieToken()Ljava/lang/String;
    .locals 1

    .line 12
    sget-object v0, Ljp/colopl/libs/Cookie;->a:Ljava/lang/String;

    return-object v0
.end method

.method public static setCookieToken(Ljava/lang/String;)V
    .locals 0

    .line 16
    sput-object p0, Ljp/colopl/libs/Cookie;->a:Ljava/lang/String;

    return-void
.end method
