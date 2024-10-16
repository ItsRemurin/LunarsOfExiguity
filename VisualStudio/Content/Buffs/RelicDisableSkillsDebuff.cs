using System;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.Skills;

namespace LunarsOfExiguity.Content.Buffs;

public class RelicDisableSkillsDebuff : BuffBase
{
    protected override string Name => "RelicDisableSkills";

    protected override bool IsStackable => true;

    protected override void Initialize()
    {
        IL.RoR2.CharacterBody.RecalculateStats += il =>
        {
            var cursor = new ILCursor(il);
            if (cursor.TryGotoNext(x => x.MatchCallOrCallvirt<CharacterBody>(nameof(CharacterBody.HandleDisableAllSkillsDebuff))))
            {
                cursor.Emit(OpCodes.Ldarg, 0);
                cursor.EmitDelegate<Action<CharacterBody>>(self =>
                {
                    if (!self.hasAuthority) return;

                    bool flag = self.HasBuff(Get());
                    if (flag != self.allSkillsDisabled)
                    {
                        var disabledSkill = LegacyResourcesAPI.Load<SkillDef>("Skills/DisabledSkills");
                        if (!disabledSkill)
                        {
                            LoELog.Warning("RelicDisableSkillsDebuff Failed to Find DisabledSkills SkillDef");
                            return;
                        }
                        
                        if (flag)
                        {
                            if (self.skillLocator.primary) self.skillLocator.primary.SetSkillOverride(self, disabledSkill, GenericSkill.SkillOverridePriority.Contextual);
                            if (self.skillLocator.secondary) self.skillLocator.secondary.SetSkillOverride(self, disabledSkill, GenericSkill.SkillOverridePriority.Contextual);
                            if (self.skillLocator.utility) self.skillLocator.utility.SetSkillOverride(self, disabledSkill, GenericSkill.SkillOverridePriority.Contextual);
                            if (self.skillLocator.special) self.skillLocator.special.SetSkillOverride(self, disabledSkill, GenericSkill.SkillOverridePriority.Contextual);
                            return;
                        }
                        
                        if (self.skillLocator.primary) self.skillLocator.primary.UnsetSkillOverride(self, disabledSkill, GenericSkill.SkillOverridePriority.Contextual);
                        if (self.skillLocator.secondary) self.skillLocator.secondary.UnsetSkillOverride(self, disabledSkill, GenericSkill.SkillOverridePriority.Contextual);
                        if (self.skillLocator.utility) self.skillLocator.utility.UnsetSkillOverride(self, disabledSkill, GenericSkill.SkillOverridePriority.Contextual);
                        if (self.skillLocator.special) self.skillLocator.special.UnsetSkillOverride(self, disabledSkill, GenericSkill.SkillOverridePriority.Contextual);
                    }
                });
                return;
            } 
            
            LoELog.Warning("Failed to Apply RelicDisableSkillsDebuff CharacterBody.RecalculateStats Hook.");
        };
    }
}