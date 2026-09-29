.class public Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;
.super Ljava/lang/Object;
.source "ColoplDepositHelper.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Ljp/colopl/drapro/ColoplDepositHelper;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "PrepareResult"
.end annotation


# instance fields
.field errorMessage:I

.field errorTitle:I

.field itemId:Ljava/lang/String;

.field payload:Ljava/lang/String;

.field statusCode:I

.field successFlag:Z

.field final synthetic this$0:Ljp/colopl/drapro/ColoplDepositHelper;


# direct methods
.method public constructor <init>(Ljp/colopl/drapro/ColoplDepositHelper;)V
    .locals 0

    .line 78
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public constructor <init>(Ljp/colopl/drapro/ColoplDepositHelper;ZILjava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 79
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->this$0:Ljp/colopl/drapro/ColoplDepositHelper;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 80
    iput-boolean p2, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->successFlag:Z

    .line 81
    iput p3, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->statusCode:I

    .line 82
    iput-object p4, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->itemId:Ljava/lang/String;

    .line 83
    iput-object p5, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->payload:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getErrorMessage()I
    .locals 1

    .line 101
    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->errorMessage:I

    return v0
.end method

.method public getErrorTitle()I
    .locals 1

    .line 98
    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->errorTitle:I

    return v0
.end method

.method public getItemId()Ljava/lang/String;
    .locals 1

    .line 92
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->itemId:Ljava/lang/String;

    return-object v0
.end method

.method public getPayload()Ljava/lang/String;
    .locals 1

    .line 95
    iget-object v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->payload:Ljava/lang/String;

    return-object v0
.end method

.method public getStatusCode()I
    .locals 1

    .line 89
    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->statusCode:I

    return v0
.end method

.method public getSuccess()Z
    .locals 1

    .line 86
    iget-boolean v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->successFlag:Z

    return v0
.end method

.method public isValidStatusCode()Z
    .locals 2

    .line 105
    iget v0, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->statusCode:I

    const/16 v1, 0x64

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public setErrorMessage(I)V
    .locals 0

    .line 102
    iput p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->errorMessage:I

    return-void
.end method

.method public setErrorTitle(I)V
    .locals 0

    .line 99
    iput p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->errorTitle:I

    return-void
.end method

.method public setItemId(Ljava/lang/String;)V
    .locals 0

    .line 93
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->itemId:Ljava/lang/String;

    return-void
.end method

.method public setPayload(Ljava/lang/String;)V
    .locals 0

    .line 96
    iput-object p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->payload:Ljava/lang/String;

    return-void
.end method

.method public setStatusCode(I)V
    .locals 0

    .line 90
    iput p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->statusCode:I

    return-void
.end method

.method public setSuccess(Z)V
    .locals 0

    .line 87
    iput-boolean p1, p0, Ljp/colopl/drapro/ColoplDepositHelper$PrepareResult;->successFlag:Z

    return-void
.end method
