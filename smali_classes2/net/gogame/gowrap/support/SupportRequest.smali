.class public Lnet/gogame/gowrap/support/SupportRequest;
.super Ljava/lang/Object;
.source "SupportRequest.java"


# instance fields
.field private final attachment:Landroid/net/Uri;

.field private final body:Ljava/lang/String;

.field private final category:Lnet/gogame/gowrap/support/SupportCategory;

.field private final email:Ljava/lang/String;

.field private final mobileNumber:Ljava/lang/String;

.field private final name:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lnet/gogame/gowrap/support/SupportCategory;Ljava/lang/String;Landroid/net/Uri;)V
    .locals 0

    .line 16
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 17
    iput-object p1, p0, Lnet/gogame/gowrap/support/SupportRequest;->name:Ljava/lang/String;

    .line 18
    iput-object p2, p0, Lnet/gogame/gowrap/support/SupportRequest;->email:Ljava/lang/String;

    .line 19
    iput-object p3, p0, Lnet/gogame/gowrap/support/SupportRequest;->mobileNumber:Ljava/lang/String;

    .line 20
    iput-object p4, p0, Lnet/gogame/gowrap/support/SupportRequest;->category:Lnet/gogame/gowrap/support/SupportCategory;

    .line 21
    iput-object p5, p0, Lnet/gogame/gowrap/support/SupportRequest;->body:Ljava/lang/String;

    .line 22
    iput-object p6, p0, Lnet/gogame/gowrap/support/SupportRequest;->attachment:Landroid/net/Uri;

    return-void
.end method


# virtual methods
.method public getAttachment()Landroid/net/Uri;
    .locals 1

    .line 46
    iget-object v0, p0, Lnet/gogame/gowrap/support/SupportRequest;->attachment:Landroid/net/Uri;

    return-object v0
.end method

.method public getBody()Ljava/lang/String;
    .locals 1

    .line 42
    iget-object v0, p0, Lnet/gogame/gowrap/support/SupportRequest;->body:Ljava/lang/String;

    return-object v0
.end method

.method public getCategory()Lnet/gogame/gowrap/support/SupportCategory;
    .locals 1

    .line 38
    iget-object v0, p0, Lnet/gogame/gowrap/support/SupportRequest;->category:Lnet/gogame/gowrap/support/SupportCategory;

    return-object v0
.end method

.method public getEmail()Ljava/lang/String;
    .locals 1

    .line 30
    iget-object v0, p0, Lnet/gogame/gowrap/support/SupportRequest;->email:Ljava/lang/String;

    return-object v0
.end method

.method public getMobileNumber()Ljava/lang/String;
    .locals 1

    .line 34
    iget-object v0, p0, Lnet/gogame/gowrap/support/SupportRequest;->mobileNumber:Ljava/lang/String;

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1

    .line 26
    iget-object v0, p0, Lnet/gogame/gowrap/support/SupportRequest;->name:Ljava/lang/String;

    return-object v0
.end method
