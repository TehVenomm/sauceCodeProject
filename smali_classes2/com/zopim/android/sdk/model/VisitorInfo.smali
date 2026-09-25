.class public Lcom/zopim/android/sdk/model/VisitorInfo;
.super Ljava/lang/Object;

# interfaces
.implements Ljava/io/Serializable;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/model/VisitorInfo$Builder;
    }
.end annotation


# static fields
.field private static final serialVersionUID:J = 0x727f65dd473c9e61L


# instance fields
.field private email:Ljava/lang/String;

.field private name:Ljava/lang/String;

.field private phoneNumber:Ljava/lang/String;


# direct methods
.method private constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private constructor <init>(Lcom/zopim/android/sdk/model/VisitorInfo$Builder;)V
    .locals 1

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    iget-object v0, p1, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->name:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/model/VisitorInfo;->name:Ljava/lang/String;

    iget-object v0, p1, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->email:Ljava/lang/String;

    iput-object v0, p0, Lcom/zopim/android/sdk/model/VisitorInfo;->email:Ljava/lang/String;

    iget-object p1, p1, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->phoneNumber:Ljava/lang/String;

    iput-object p1, p0, Lcom/zopim/android/sdk/model/VisitorInfo;->phoneNumber:Ljava/lang/String;

    return-void
.end method

.method synthetic constructor <init>(Lcom/zopim/android/sdk/model/VisitorInfo$Builder;Lcom/zopim/android/sdk/model/b;)V
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/model/VisitorInfo;-><init>(Lcom/zopim/android/sdk/model/VisitorInfo$Builder;)V

    return-void
.end method


# virtual methods
.method public getEmail()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/VisitorInfo;->email:Ljava/lang/String;

    return-object v0
.end method

.method public getName()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/VisitorInfo;->name:Ljava/lang/String;

    return-object v0
.end method

.method public getPhoneNumber()Ljava/lang/String;
    .locals 1
    .annotation build Landroidx/annotation/Nullable;
    .end annotation

    iget-object v0, p0, Lcom/zopim/android/sdk/model/VisitorInfo;->phoneNumber:Ljava/lang/String;

    return-object v0
.end method

.method public setEmail(Ljava/lang/String;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/model/VisitorInfo;->email:Ljava/lang/String;

    return-void
.end method

.method public setName(Ljava/lang/String;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/model/VisitorInfo;->name:Ljava/lang/String;

    return-void
.end method

.method public setPhoneNumber(Ljava/lang/String;)V
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/model/VisitorInfo;->phoneNumber:Ljava/lang/String;

    return-void
.end method
