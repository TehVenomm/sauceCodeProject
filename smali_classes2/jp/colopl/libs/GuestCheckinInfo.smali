.class public Ljp/colopl/libs/GuestCheckinInfo;
.super Ljava/lang/Object;
.source "GuestCheckinInfo.java"


# instance fields
.field private mAppId:Ljava/lang/String;

.field private mGuestUserId:Ljava/lang/String;

.field private mHash:Ljava/lang/String;

.field private mTime:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 9
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 10
    iput-object p1, p0, Ljp/colopl/libs/GuestCheckinInfo;->mAppId:Ljava/lang/String;

    .line 11
    iput-object p2, p0, Ljp/colopl/libs/GuestCheckinInfo;->mGuestUserId:Ljava/lang/String;

    .line 12
    iput-object p3, p0, Ljp/colopl/libs/GuestCheckinInfo;->mTime:Ljava/lang/String;

    .line 13
    iput-object p4, p0, Ljp/colopl/libs/GuestCheckinInfo;->mHash:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getAppId()Ljava/lang/String;
    .locals 1

    .line 17
    iget-object v0, p0, Ljp/colopl/libs/GuestCheckinInfo;->mAppId:Ljava/lang/String;

    return-object v0
.end method

.method public getGuestUserId()Ljava/lang/String;
    .locals 1

    .line 21
    iget-object v0, p0, Ljp/colopl/libs/GuestCheckinInfo;->mGuestUserId:Ljava/lang/String;

    return-object v0
.end method

.method public getHash()Ljava/lang/String;
    .locals 1

    .line 29
    iget-object v0, p0, Ljp/colopl/libs/GuestCheckinInfo;->mHash:Ljava/lang/String;

    return-object v0
.end method

.method public getTime()Ljava/lang/String;
    .locals 1

    .line 25
    iget-object v0, p0, Ljp/colopl/libs/GuestCheckinInfo;->mTime:Ljava/lang/String;

    return-object v0
.end method
