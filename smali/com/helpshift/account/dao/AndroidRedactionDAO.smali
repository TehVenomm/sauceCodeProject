.class public Lcom/helpshift/account/dao/AndroidRedactionDAO;
.super Ljava/lang/Object;
.source "AndroidRedactionDAO.java"

# interfaces
.implements Lcom/helpshift/redaction/RedactionDAO;


# instance fields
.field private final userDB:Lcom/helpshift/account/dao/UserDB;


# direct methods
.method public constructor <init>(Lcom/helpshift/account/dao/UserDB;)V
    .locals 0

    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 15
    iput-object p1, p0, Lcom/helpshift/account/dao/AndroidRedactionDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    return-void
.end method


# virtual methods
.method public deleteRedactionDetail(J)V
    .locals 3

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-lez v2, :cond_0

    .line 64
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidRedactionDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/account/dao/UserDB;->deleteRedactionDetail(J)V

    :cond_0
    return-void
.end method

.method public getRedactionDetail(J)Lcom/helpshift/redaction/RedactionDetail;
    .locals 3

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-gez v2, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 25
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidRedactionDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1, p2}, Lcom/helpshift/account/dao/UserDB;->fetchRedactionDetail(J)Lcom/helpshift/redaction/RedactionDetail;

    move-result-object p1

    return-object p1
.end method

.method public insertRedactionDetail(Lcom/helpshift/redaction/RedactionDetail;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 36
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidRedactionDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->insertRedactionDetail(Lcom/helpshift/redaction/RedactionDetail;)V

    return-void
.end method

.method public updateRedactionRedail(Lcom/helpshift/redaction/RedactionDetail;)V
    .locals 1

    if-nez p1, :cond_0

    return-void

    .line 47
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidRedactionDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1}, Lcom/helpshift/account/dao/UserDB;->updateRedactionDetail(Lcom/helpshift/redaction/RedactionDetail;)V

    return-void
.end method

.method public updateRedactionState(JLcom/helpshift/redaction/RedactionState;)V
    .locals 3

    const-wide/16 v0, 0x0

    cmp-long v2, p1, v0

    if-ltz v2, :cond_1

    if-nez p3, :cond_0

    goto :goto_0

    .line 58
    :cond_0
    iget-object v0, p0, Lcom/helpshift/account/dao/AndroidRedactionDAO;->userDB:Lcom/helpshift/account/dao/UserDB;

    invoke-virtual {v0, p1, p2, p3}, Lcom/helpshift/account/dao/UserDB;->updateRedactionState(JLcom/helpshift/redaction/RedactionState;)V

    return-void

    :cond_1
    :goto_0
    return-void
.end method
