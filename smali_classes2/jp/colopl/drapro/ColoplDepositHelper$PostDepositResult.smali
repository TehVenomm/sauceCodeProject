.class public Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;
.super Ljava/lang/Object;
.source "ColoplDepositHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Ljp/colopl/drapro/ColoplDepositHelper;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "PostDepositResult"
.end annotation


# instance fields
.field errorMessage:I

.field errorTitle:I

.field purchase:Ljp/colopl/iab/Purchase;

.field purchasedSku:Ljava/lang/String;

.field resultData:Ljava/lang/String;

.field statusCode:I

.field successFlag:Z

.field final synthetic this$0:Ljp/colopl/drapro/ColoplDepositHelper;


# direct methods
.method public constructor <init>(Ljp/colopl/drapro/ColoplDepositHelper;)V
    .locals 0

    .line 119
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public getErrorMessage()I
    .locals 1

    .line 136
    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->errorMessage:I

    return v0
.end method

.method public getErrorTitle()I
    .locals 1

    .line 133
    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->errorTitle:I

    return v0
.end method

.method public getPurchase()Ljp/colopl/iab/Purchase;
    .locals 1

    .line 139
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->purchase:Ljp/colopl/iab/Purchase;

    return-object v0
.end method

.method public getPurchasedSku()Ljava/lang/String;
    .locals 1

    .line 127
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->purchasedSku:Ljava/lang/String;

    return-object v0
.end method

.method public getResultData()Ljava/lang/String;
    .locals 1

    .line 130
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->resultData:Ljava/lang/String;

    return-object v0
.end method

.method public getStatusCode()I
    .locals 1

    .line 124
    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->statusCode:I

    return v0
.end method

.method public getSuccess()Z
    .locals 1

    .line 121
    iget-boolean v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->successFlag:Z

    return v0
.end method

.method public isAlreadyCancelled()Z
    .locals 2

    .line 148
    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->statusCode:I

    const/16 v1, 0x191

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isValidStatusCode()Z
    .locals 2

    .line 144
    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->statusCode:I

    const/16 v1, 0x64

    if-eq v0, v1, :cond_1

    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->statusCode:I

    const/16 v1, 0x12c

    if-eq v0, v1, :cond_1

    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->statusCode:I

    const/16 v1, 0xcb

    if-ne v0, v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v0, 0x1

    :goto_1
    return v0
.end method

.method public setErrorMessage(I)V
    .locals 0

    .line 137
    iput p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->errorMessage:I

    return-void
.end method

.method public setErrorTitle(I)V
    .locals 0

    .line 134
    iput p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->errorTitle:I

    return-void
.end method

.method public setPurchase(Ljp/colopl/iab/Purchase;)V
    .locals 0

    .line 140
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->purchase:Ljp/colopl/iab/Purchase;

    return-void
.end method

.method public setPurchasedSku(Ljava/lang/String;)V
    .locals 0

    .line 128
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->purchasedSku:Ljava/lang/String;

    return-void
.end method

.method public setResultData(Ljava/lang/String;)V
    .locals 0

    .line 131
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->resultData:Ljava/lang/String;

    return-void
.end method

.method public setStatusCode(I)V
    .locals 0

    .line 125
    iput p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->statusCode:I

    return-void
.end method

.method public setSuccess(Z)V
    .locals 0

    .line 122
    iput-boolean p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PostDepositResult;->successFlag:Z

    return-void
.end method
