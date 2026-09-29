.class public Ljp/colopl/api/docomo/ResultInfo;
.super Ljava/lang/Object;
.source "ResultInfo.java"


# instance fields
.field private errorMessage:Ljava/lang/String;

.field private resultCode:I

.field private totalCount:I


# direct methods
.method public constructor <init>()V
    .locals 1

    .line 13
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 4
    iput v0, p0, Ljp/colopl/api/docomo/ResultInfo;->totalCount:I

    .line 5
    iput v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/4 v0, 0x0

    .line 6
    iput-object v0, p0, Ljp/colopl/api/docomo/ResultInfo;->errorMessage:Ljava/lang/String;

    return-void
.end method

.method constructor <init>(IILjava/lang/String;)V
    .locals 1

    .line 8
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const/4 v0, 0x0

    .line 4
    iput v0, p0, Ljp/colopl/api/docomo/ResultInfo;->totalCount:I

    .line 5
    iput v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/4 v0, 0x0

    .line 6
    iput-object v0, p0, Ljp/colopl/api/docomo/ResultInfo;->errorMessage:Ljava/lang/String;

    .line 9
    iput p1, p0, Ljp/colopl/api/docomo/ResultInfo;->totalCount:I

    .line 10
    iput p2, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    .line 11
    iput-object p3, p0, Ljp/colopl/api/docomo/ResultInfo;->errorMessage:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public getErrorMessage()Ljava/lang/String;
    .locals 1

    .line 27
    iget-object v0, p0, Ljp/colopl/api/docomo/ResultInfo;->errorMessage:Ljava/lang/String;

    return-object v0
.end method

.method public getResultCode()I
    .locals 1

    .line 26
    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    return v0
.end method

.method public getStatus()I
    .locals 3

    .line 30
    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0x7d0

    if-lt v0, v1, :cond_0

    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0xbb7

    if-gt v0, v1, :cond_0

    const/4 v0, 0x0

    return v0

    .line 33
    :cond_0
    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0xbb8

    const/4 v2, 0x2

    if-lt v0, v1, :cond_1

    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0xf9f

    if-gt v0, v1, :cond_1

    return v2

    .line 36
    :cond_1
    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0xfa0

    if-lt v0, v1, :cond_2

    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0x1387

    if-gt v0, v1, :cond_2

    const/4 v0, 0x3

    return v0

    .line 39
    :cond_2
    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0x1388

    if-lt v0, v1, :cond_3

    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0x176f

    if-gt v0, v1, :cond_3

    return v2

    :cond_3
    const/4 v0, -0x1

    return v0
.end method

.method public getTotalCount()I
    .locals 1

    .line 25
    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->totalCount:I

    return v0
.end method

.method public getUrlForAuthorize()Ljava/lang/String;
    .locals 1

    .line 59
    invoke-virtual {p0}, Ljp/colopl/api/docomo/ResultInfo;->isNeedToAuthroize()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 60
    iget-object v0, p0, Ljp/colopl/api/docomo/ResultInfo;->errorMessage:Ljava/lang/String;

    return-object v0

    .line 62
    :cond_0
    invoke-virtual {p0}, Ljp/colopl/api/docomo/ResultInfo;->isNeedToAuthroizeAll()Z

    move-result v0

    if-eqz v0, :cond_1

    const-string v0, "https://spmode.ne.jp/setting/apiControlAuthKeyClean.do"

    return-object v0

    :cond_1
    const/4 v0, 0x0

    return-object v0
.end method

.method public isNeedToAuthroize()Z
    .locals 2

    .line 50
    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0xfa2

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isNeedToAuthroizeAll()Z
    .locals 2

    .line 55
    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0x1004

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public isResultCodeOK()Z
    .locals 2

    .line 46
    iget v0, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    const/16 v1, 0x7d0

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    goto :goto_0

    :cond_0
    const/4 v0, 0x0

    :goto_0
    return v0
.end method

.method public setErrorMessage(Ljava/lang/String;)V
    .locals 0

    .line 23
    iput-object p1, p0, Ljp/colopl/api/docomo/ResultInfo;->errorMessage:Ljava/lang/String;

    return-void
.end method

.method public setResultCode(I)V
    .locals 0

    .line 20
    iput p1, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    return-void
.end method

.method public setTotalCount(I)V
    .locals 0

    .line 17
    iput p1, p0, Ljp/colopl/api/docomo/ResultInfo;->totalCount:I

    return-void
.end method

.method public toString()Ljava/lang/String;
    .locals 3

    .line 69
    new-instance v0, Ljava/lang/StringBuffer;

    invoke-direct {v0}, Ljava/lang/StringBuffer;-><init>()V

    .line 70
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "[totalCount: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v2, p0, Ljp/colopl/api/docomo/ResultInfo;->totalCount:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 71
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", resultCode: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget v2, p0, Ljp/colopl/api/docomo/ResultInfo;->resultCode:I

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 72
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, ", errorMessage: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v2, p0, Ljp/colopl/api/docomo/ResultInfo;->errorMessage:Ljava/lang/String;

    if-nez v2, :cond_0

    const-string v2, "null"

    goto :goto_0

    :cond_0
    iget-object v2, p0, Ljp/colopl/api/docomo/ResultInfo;->errorMessage:Ljava/lang/String;

    :goto_0
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    const-string v1, "]"

    .line 73
    invoke-virtual {v0, v1}, Ljava/lang/StringBuffer;->append(Ljava/lang/String;)Ljava/lang/StringBuffer;

    .line 74
    invoke-virtual {v0}, Ljava/lang/StringBuffer;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
