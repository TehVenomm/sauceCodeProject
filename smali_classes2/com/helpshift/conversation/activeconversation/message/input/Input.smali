.class public Lcom/helpshift/conversation/activeconversation/message/input/Input;
.super Ljava/lang/Object;
.source "Input.java"


# instance fields
.field public final botInfo:Ljava/lang/String;

.field public final inputLabel:Ljava/lang/String;

.field public final required:Z

.field public final skipLabel:Ljava/lang/String;


# direct methods
.method public constructor <init>(Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;)V
    .locals 0

    .line 12
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 13
    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/input/Input;->botInfo:Ljava/lang/String;

    .line 14
    iput-boolean p2, p0, Lcom/helpshift/conversation/activeconversation/message/input/Input;->required:Z

    .line 15
    iput-object p3, p0, Lcom/helpshift/conversation/activeconversation/message/input/Input;->inputLabel:Ljava/lang/String;

    .line 16
    iput-object p4, p0, Lcom/helpshift/conversation/activeconversation/message/input/Input;->skipLabel:Ljava/lang/String;

    return-void
.end method


# virtual methods
.method public equals(Ljava/lang/Object;)Z
    .locals 3

    .line 21
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/Input;

    const/4 v1, 0x0

    if-nez v0, :cond_0

    return v1

    .line 25
    :cond_0
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/input/Input;

    .line 26
    iget-boolean v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/Input;->required:Z

    iget-boolean v2, p0, Lcom/helpshift/conversation/activeconversation/message/input/Input;->required:Z

    if-ne v0, v2, :cond_1

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/Input;->inputLabel:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/input/Input;->inputLabel:Ljava/lang/String;

    .line 27
    invoke-static {v0, v2}, Lcom/helpshift/common/StringUtils;->isEqual(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object v0, p1, Lcom/helpshift/conversation/activeconversation/message/input/Input;->skipLabel:Ljava/lang/String;

    iget-object v2, p0, Lcom/helpshift/conversation/activeconversation/message/input/Input;->skipLabel:Ljava/lang/String;

    .line 28
    invoke-static {v0, v2}, Lcom/helpshift/common/StringUtils;->isEqual(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/input/Input;->botInfo:Ljava/lang/String;

    iget-object v0, p0, Lcom/helpshift/conversation/activeconversation/message/input/Input;->botInfo:Ljava/lang/String;

    .line 29
    invoke-static {p1, v0}, Lcom/helpshift/common/StringUtils;->isEqual(Ljava/lang/String;Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_1

    const/4 v1, 0x1

    :cond_1
    return v1
.end method
