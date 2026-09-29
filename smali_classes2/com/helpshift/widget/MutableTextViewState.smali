.class public Lcom/helpshift/widget/MutableTextViewState;
.super Lcom/helpshift/widget/TextViewState;
.source "MutableTextViewState.java"


# direct methods
.method public constructor <init>(Z)V
    .locals 0

    .line 6
    invoke-direct {p0, p1}, Lcom/helpshift/widget/TextViewState;-><init>(Z)V

    return-void
.end method


# virtual methods
.method public setError(Lcom/helpshift/widget/TextViewState$TextViewStatesError;)V
    .locals 0

    .line 10
    iput-object p1, p0, Lcom/helpshift/widget/MutableTextViewState;->error:Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    .line 11
    invoke-virtual {p0, p0}, Lcom/helpshift/widget/MutableTextViewState;->notifyChange(Ljava/lang/Object;)V

    return-void
.end method

.method public setText(Ljava/lang/String;)V
    .locals 1

    .line 15
    invoke-virtual {p0}, Lcom/helpshift/widget/MutableTextViewState;->getText()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    .line 16
    iput-object p1, p0, Lcom/helpshift/widget/MutableTextViewState;->text:Ljava/lang/String;

    .line 17
    invoke-virtual {p0}, Lcom/helpshift/widget/MutableTextViewState;->getError()Lcom/helpshift/widget/TextViewState$TextViewStatesError;

    move-result-object p1

    if-eqz p1, :cond_0

    const/4 p1, 0x0

    .line 18
    invoke-virtual {p0, p1}, Lcom/helpshift/widget/MutableTextViewState;->setError(Lcom/helpshift/widget/TextViewState$TextViewStatesError;)V

    :cond_0
    return-void
.end method
