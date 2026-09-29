.class public Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;
.super Lcom/helpshift/conversation/activeconversation/message/input/Input;
.source "OptionInput.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;,
        Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;
    }
.end annotation


# instance fields
.field public final options:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List<",
            "Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Option;",
            ">;"
        }
    .end annotation
.end field

.field public final type:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;


# direct methods
.method public constructor <init>(Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;Ljava/util/List;Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
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

    .line 14
    invoke-direct {p0, p1, p2, p3, p4}, Lcom/helpshift/conversation/activeconversation/message/input/Input;-><init>(Ljava/lang/String;ZLjava/lang/String;Ljava/lang/String;)V

    .line 15
    iput-object p5, p0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->options:Ljava/util/List;

    .line 16
    iput-object p6, p0, Lcom/helpshift/conversation/activeconversation/message/input/OptionInput;->type:Lcom/helpshift/conversation/activeconversation/message/input/OptionInput$Type;

    return-void
.end method
