.class public Lcom/zopim/android/sdk/model/VisitorInfo$Builder;
.super Ljava/lang/Object;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/zopim/android/sdk/model/VisitorInfo;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Builder"
.end annotation


# instance fields
.field email:Ljava/lang/String;

.field name:Ljava/lang/String;

.field phoneNumber:Ljava/lang/String;


# direct methods
.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public build()Lcom/zopim/android/sdk/model/VisitorInfo;
    .locals 2

    new-instance v0, Lcom/zopim/android/sdk/model/VisitorInfo;

    const/4 v1, 0x0

    invoke-direct {v0, p0, v1}, Lcom/zopim/android/sdk/model/VisitorInfo;-><init>(Lcom/zopim/android/sdk/model/VisitorInfo$Builder;Lcom/zopim/android/sdk/model/b;)V

    return-object v0
.end method

.method public email(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->email:Ljava/lang/String;

    return-object p0
.end method

.method public name(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->name:Ljava/lang/String;

    return-object p0
.end method

.method public phoneNumber(Ljava/lang/String;)Lcom/zopim/android/sdk/model/VisitorInfo$Builder;
    .locals 0

    iput-object p1, p0, Lcom/zopim/android/sdk/model/VisitorInfo$Builder;->phoneNumber:Ljava/lang/String;

    return-object p0
.end method
