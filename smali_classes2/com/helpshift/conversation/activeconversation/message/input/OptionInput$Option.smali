.class public Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;
.super Ljava/lang/Object;
.source "OptionInput.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "Option"
.end annotation


# instance fields
.field public final jsonData:Ljava/lang/String;

.field public final title:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 24
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 25
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->title:Ljava/lang/String;

    .line 26
    iput-object p2, p0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->jsonData:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 3

    .line 37
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 41
    :cond_0
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;

    .line 43
    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->title:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->title:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->jsonData:Ljava/lang/String;

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;->jsonData:Ljava/lang/String;

    invoke-virtual {p1, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    const/4 v1, 0x1

    :cond_1
    return v1
.end method
