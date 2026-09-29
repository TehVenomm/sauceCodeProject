.class public Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;
.super Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;
.source "AdminMessageWithOptionInputDM.java"


# instance fields
.field public input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;)V
    .locals 8
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "J",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Z",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;",
            ">;",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;",
            ")V"
        }
    .end annotation

    .line 17
    sget-object v7, Lcom/helpshift/conversation/activeconversation/message/MessageType;->ADMIN_TEXT_WITH_OPTION_INPUT:Lcom/helpshift/conversation/activeconversation/message/MessageType;

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-wide v4, p4

    move-object v6, p6

    invoke-direct/range {v0 .. v7}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;JLjava/lang/String;Lcom/helpshift/conversation/activeconversation/message/MessageType;)V

    .line 18
    new-instance v7, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    move-object v0, v7

    move-object v1, p7

    move/from16 v2, p8

    move-object/from16 v3, p9

    move-object/from16 v4, p10

    move-object/from16 v5, p11

    move-object/from16 v6, p12

    invoke-direct/range {v0 .. v6}, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;-><init>(Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;)V

    move-object v0, p0

    iput-object v7, v0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    return-void
.end method


# virtual methods
.method public merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V
    .locals 1

    .line 23
    invoke-super {p0, p1}, Lcom/helpshift/conversation/activeconversation/message/AdminMessageDM;->merge(Lcom/helpshift/conversation/activeconversation/message/MessageDM;)V

    .line 24
    instance-of v0, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;

    if-eqz v0, :cond_0

    .line 25
    check-cast p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;

    .line 26
    iget-object p1, p1, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    iput-object p1, p0, Lcom/helpshift/conversation/activeconversation/message/AdminMessageWithOptionInputDM;->input:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;

    :cond_0
    return-void
.end method
